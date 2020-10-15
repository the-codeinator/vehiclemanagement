using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace VehicleManagement.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "newId()"),
                    created_at = table.Column<DateTime>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    Make = table.Column<int>(nullable: false),
                    Model = table.Column<string>(maxLength: 40, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Brand = table.Column<string>(maxLength: 40, nullable: false),
                    Doors = table.Column<int>(nullable: false),
                    BodyType = table.Column<int>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");
        }
    }
}
