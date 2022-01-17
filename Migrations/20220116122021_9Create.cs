using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blog2.Migrations
{
    public partial class _9Create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "BlogsDb",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "BlogsDb");
        }
    }
}
