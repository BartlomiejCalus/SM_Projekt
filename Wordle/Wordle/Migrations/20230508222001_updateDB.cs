using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Migrations
{
    public partial class updateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodayPlays",
                table: "UserStat",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TodayPlays",
                table: "UserStat");
        }
    }
}
