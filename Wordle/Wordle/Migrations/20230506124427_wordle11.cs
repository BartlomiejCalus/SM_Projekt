using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wordle.Migrations
{
    public partial class wordle11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TopPointsStat",
                columns: table => new
                {
                    topID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    points = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopPointsStat", x => x.topID);
                    table.ForeignKey(
                        name: "FK_TopPointsStat_AspNetUsers_userID",
                        column: x => x.userID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopPointsStat_userID",
                table: "TopPointsStat",
                column: "userID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopPointsStat");
        }
    }
}
