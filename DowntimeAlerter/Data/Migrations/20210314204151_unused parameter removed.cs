using Microsoft.EntityFrameworkCore.Migrations;

namespace DowntimeAlerter.Data.Migrations
{
    public partial class unusedparameterremoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExitAfterException",
                table: "TargetApplication");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ExitAfterException",
                table: "TargetApplication",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
