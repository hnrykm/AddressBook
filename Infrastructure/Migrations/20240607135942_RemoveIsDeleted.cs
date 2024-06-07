using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Interview.Api.Migrations
{
    public partial class RemoveIsDeleted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "People",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
