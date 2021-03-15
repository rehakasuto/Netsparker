using System.Net;

namespace DowntimeAlerter.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Send HTTP Get with given url
        /// </summary>
        /// <param name="url">
        /// Requested url
        /// </param>
        /// <returns></returns>
        public static HttpStatusCode GetData(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                return response.StatusCode;
            }
        }
    }
}
