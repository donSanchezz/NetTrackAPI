using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetTrackAPI.Migrations
{
    public partial class RemoveCountryFromUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "Country",
                table: "AspNetUsers");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

        }
    }
}
