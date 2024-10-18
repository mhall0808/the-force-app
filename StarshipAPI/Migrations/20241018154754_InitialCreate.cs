using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarshipAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EpisodeId = table.Column<int>(type: "int", nullable: true),
                    OpeningCrawl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Characters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Planets = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Starships = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HairColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkinColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EyeColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Homeworld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Vehicles = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Starships = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Planets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RotationPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrbitalPeriod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diameter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Climate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gravity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Terrain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurfaceWater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Population = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Residents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageHeight = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkinColors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HairColors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EyeColors = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AverageLifespan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Homeworld = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    People = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Starships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostInCredits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAtmospheringSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passengers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HyperdriveRating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MGLT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StarshipClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pilots = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Starships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostInCredits = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxAtmospheringSpeed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Crew = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passengers = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CargoCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Consumables = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pilots = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Films = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Edited = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Planets");

            migrationBuilder.DropTable(
                name: "Species");

            migrationBuilder.DropTable(
                name: "Starships");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
