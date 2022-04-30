using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElectricityWebApp.Migrations
{
    public partial class livedb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countryData",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CountryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countryData", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "electricityDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CountryID = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    UnitsConsumption = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_electricityDetails", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "series",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    series_id = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    units = table.Column<string>(nullable: true),
                    f = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    source = table.Column<string>(nullable: true),
                    iso3166 = table.Column<string>(nullable: true),
                    latlon = table.Column<string>(nullable: true),
                    latlon2 = table.Column<string>(nullable: true),
                    geography = table.Column<string>(nullable: true),
                    geography2 = table.Column<string>(nullable: true),
                    lastHistoricalPeriod = table.Column<string>(nullable: true),
                    start = table.Column<string>(nullable: true),
                    end = table.Column<string>(nullable: true),
                    updated = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_series", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "countryData");

            migrationBuilder.DropTable(
                name: "electricityDetails");

            migrationBuilder.DropTable(
                name: "series");
        }
    }
}
