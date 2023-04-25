using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Migrations
{
    public partial class Wordle3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "points",
                table: "GameStat",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "quessTime",
                table: "GameStat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "startTime",
                table: "GameStat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "tries",
                table: "GameStat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "points",
                table: "GameStat");

            migrationBuilder.DropColumn(
                name: "quessTime",
                table: "GameStat");

            migrationBuilder.DropColumn(
                name: "startTime",
                table: "GameStat");

            migrationBuilder.DropColumn(
                name: "tries",
                table: "GameStat");
        }
    }
}
