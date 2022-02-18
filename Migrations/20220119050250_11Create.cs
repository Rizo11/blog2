using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog2.Migrations
{
    public partial class _11Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Accepted",
                table: "BlogsDb");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BlogsDb",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "BlogsDb");

            migrationBuilder.AddColumn<bool>(
                name: "Accepted",
                table: "BlogsDb",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
