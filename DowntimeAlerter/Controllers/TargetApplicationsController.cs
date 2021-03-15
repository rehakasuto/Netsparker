using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DowntimeAlerter.Data;
using DowntimeAlerter.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Hangfire;
using DowntimeAlerter.Helpers;
using DowntimeAlerter.Utilities.Notifications;
using Microsoft.AspNetCore.Identity;
using Hangfire.Common;

namespace DowntimeAlerter.Controllers
{
    [Authorize]
    public class TargetApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly NotificationManager _notificationManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TargetApplicationsController(
            ApplicationDbContext context,
            NotificationManager notificationManager,
            UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _notificationManager = notificationManager;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: TargetApplications
        public async Task<IActionResult> Index()
        {
            var userIdentifier = GetUserIdentifier();
            var targetApplications = _context.TargetApplication
                    .Where(ta => ta.UserId == userIdentifier)
                    .ToListAsync();

            return View(await targetApplications);
        }

        // GET: TargetApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var userIdentifier = GetUserIdentifier();
            var targetApplication = await _context.TargetApplication
                .FirstOrDefaultAsync(ta =>
                    ta.Id == id &&
                    ta.UserId == userIdentifier);

            IdentityUser user = _userManager.FindByIdAsync(userIdentifier).Result;

            var notificationInformation = new NotificationInformation(user, "message");
            _notificationManager.SendAll(notificationInformation);


            if (targetApplication == null)
            {
                return NotFound();
            }

            return View(targetApplication);
        }

        // GET: TargetApplications/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TargetApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Url,Interval,Id")] TargetApplication targetApplication)
        {
            if (ModelState.IsValid)
            {
                targetApplication.ModifiedDate = DateTime.Now;
                targetApplication.CreatedDate = DateTime.Now;

                var userIdentifier = GetUserIdentifier();
                if (string.IsNullOrEmpty(userIdentifier))
                {
                    return NotFound();
                }

                var isExists = _context.TargetApplication
                    .FirstOrDefault(x =>
                         x.UserId == userIdentifier &&
                        (x.Name.Equals(targetApplication.Name) || x.Url.Equals(targetApplication.Url))) != null;

                if (isExists)
                {
                    return BadRequest("Target application is already exists.");
                }

                targetApplication.UserId = userIdentifier;

                _context.Add(targetApplication);
                await _context.SaveChangesAsync();

                IdentityUser user = _userManager.FindByIdAsync(userIdentifier).Result;

                var manager = new RecurringJobManager();
                manager.AddOrUpdate(
                    $"{userIdentifier}-{targetApplication.Id}-{targetApplication.Name}",
                    Job.FromExpression(() => CheckTartgetApplicationHealth(targetApplication.Url, user)),
                    targetApplication.Interval,
                    TimeZoneInfo.Local);

                return RedirectToAction(nameof(Index));
            }
            return View(targetApplication);
        }

        // GET: TargetApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var targetApplication = await _context.TargetApplication.FindAsync(id);
            if (targetApplication == null)
            {
                return NotFound();
            }
            return View(targetApplication);
        }

        // POST: TargetApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Url,Interval,Id,CreatedDate,UserId")] TargetApplication targetApplication)
        {
            if (id != targetApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var userIdentifier = GetUserIdentifier();

                    targetApplication.ModifiedDate = DateTime.Now;
                    _context.Update(targetApplication);
                    await _context.SaveChangesAsync();

                    IdentityUser user = _userManager.FindByIdAsync(userIdentifier).Result;

                    var manager = new RecurringJobManager();
                    manager.AddOrUpdate(
                        $"{userIdentifier}-{targetApplication.Id}-{targetApplication.Name}",
                        Job.FromExpression(() => CheckTartgetApplicationHealth(targetApplication.Url, user)),
                        targetApplication.Interval,
                        TimeZoneInfo.Local);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TargetApplicationExists(targetApplication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(targetApplication);
        }

        // GET: TargetApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var targetApplication = await _context.TargetApplication
                .FirstOrDefaultAsync(m => m.Id == id);
            if (targetApplication == null)
            {
                return NotFound();
            }

            return View(targetApplication);
        }

        // POST: TargetApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userIdentifier = GetUserIdentifier();

            var targetApplication = await _context.TargetApplication.FindAsync(id);
            _context.TargetApplication.Remove(targetApplication);
            await _context.SaveChangesAsync();

            var manager = new RecurringJobManager();
            manager.RemoveIfExists($"{userIdentifier}-{targetApplication.Id}-{targetApplication.Name}");

            return RedirectToAction(nameof(Index));
        }


        #region Helpers
        /// <summary>
        /// Check target application exists or not with given id
        /// </summary>
        /// <param name="id">
        /// Identifier of target application
        /// </param>
        /// <returns></returns>
        private bool TargetApplicationExists(int id)
        {
            return _context.TargetApplication.Any(e => e.Id == id);
        }

        /// <summary>
        /// Get user identifier from claim
        /// </summary>
        /// <returns></returns>
        private string GetUserIdentifier()
        {
            return _httpContextAccessor.HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
                    .Value;
        }

        /// <summary>
        /// Check application periodically and investigate down or up
        /// If response different from 2xx codes send notification to user
        /// </summary>
        /// <param name="url">
        /// App url
        /// </param>
        /// <param name="user">
        /// Current user
        /// </param>
        public async Task CheckTartgetApplicationHealth(
            string url,
            IdentityUser user)
        {
            var httpStatusCode = Extensions.GetData(url);

            if (httpStatusCode != System.Net.HttpStatusCode.OK &&
                httpStatusCode != System.Net.HttpStatusCode.NoContent &&
                httpStatusCode != System.Net.HttpStatusCode.Created)
            {
                var notificationInformation = new NotificationInformation(user, "");
                _notificationManager.SendAll(notificationInformation);
            }
        }
        #endregion
    }
}
