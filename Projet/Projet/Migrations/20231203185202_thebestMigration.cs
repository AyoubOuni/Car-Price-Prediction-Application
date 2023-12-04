using Microsoft.EntityFrameworkCore.Migrations;

namespace Projet.Migrations
{
    public partial class thebestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPredictions_users_UserId1",
                table: "CarPredictions");

            migrationBuilder.DropIndex(
                name: "IX_CarPredictions_UserId1",
                table: "CarPredictions");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CarPredictions");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CarPredictions",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext CHARACTER SET utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CarPredictions_UserId",
                table: "CarPredictions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPredictions_users_UserId",
                table: "CarPredictions",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarPredictions_users_UserId",
                table: "CarPredictions");

            migrationBuilder.DropIndex(
                name: "IX_CarPredictions_UserId",
                table: "CarPredictions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CarPredictions",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "CarPredictions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarPredictions_UserId1",
                table: "CarPredictions",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CarPredictions_users_UserId1",
                table: "CarPredictions",
                column: "UserId1",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
