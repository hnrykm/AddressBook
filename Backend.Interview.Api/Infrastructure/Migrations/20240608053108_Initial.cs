using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Interview.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Address_Line1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address_Line2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Address_City = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Address_State = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Address_ZipCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
