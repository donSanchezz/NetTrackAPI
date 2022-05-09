using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetTrackAPI.Migrations
{
    public partial class addStatusToAlert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "status",
                table: "Alert",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Alert");
        }
    }
}
