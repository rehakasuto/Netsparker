using Microsoft.EntityFrameworkCore.Migrations;

namespace DowntimeAlerter.Data.Migrations
{
    public partial class look : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TargetApplication",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TargetApplication");
        }
    }
}
