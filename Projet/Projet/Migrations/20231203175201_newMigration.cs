using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projet.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarPredictions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Make = table.Column<string>(nullable: false),
                    Symboling = table.Column<int>(nullable: false),
                    FuelType = table.Column<string>(nullable: false),
                    Aspiration = table.Column<string>(nullable: false),
                    NumOfDoors = table.Column<int>(nullable: false),
                    BodyStyle = table.Column<string>(nullable: false),
                    DriveWheels = table.Column<string>(nullable: false),
                    EngineType = table.Column<string>(nullable: false),
                    NumOfCylinders = table.Column<int>(nullable: false),
                    FuelSystem = table.Column<string>(nullable: false),
                    Horsepower = table.Column<int>(nullable: false),
                    PeakRpm = table.Column<int>(nullable: false),
                    CityMPG = table.Column<int>(nullable: false),
                    HighwayMPG = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarPredictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarPredictions_users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarPredictions_UserId1",
                table: "CarPredictions",
                column: "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarPredictions");
        }
    }
}
