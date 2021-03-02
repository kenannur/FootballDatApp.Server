using Microsoft.EntityFrameworkCore.Migrations;

namespace Vegas.FootballDatApp.Migrations
{
    public partial class UpdatePlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Players",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Players");
        }
    }
}
