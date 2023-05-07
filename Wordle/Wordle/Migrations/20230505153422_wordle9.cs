using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Migrations
{
    public partial class wordle9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "averagePlayTime",
                table: "UserStat",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<long>(
                name: "checks",
                table: "UserStat",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "fastestWin",
                table: "UserStat",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<long>(
                name: "finishes",
                table: "UserStat",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "wins",
                table: "UserStat",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "averagePlayTime",
                table: "UserStat");

            migrationBuilder.DropColumn(
                name: "checks",
                table: "UserStat");

            migrationBuilder.DropColumn(
                name: "fastestWin",
                table: "UserStat");

            migrationBuilder.DropColumn(
                name: "finishes",
                table: "UserStat");

            migrationBuilder.DropColumn(
                name: "wins",
                table: "UserStat");
        }
    }
}
