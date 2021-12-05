using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HT_RESTful_Lite.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cups",
                columns: table => new
                {
                    CupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cups", x => x.CupId);
                });

            migrationBuilder.CreateTable(
                name: "Leagues",
                columns: table => new
                {
                    LeagueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leagues", x => x.LeagueId);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamId);
                });

            migrationBuilder.CreateTable(
                name: "CupDetails",
                columns: table => new
                {
                    CupId = table.Column<int>(type: "int", nullable: false),
                    Round = table.Column<int>(type: "int", nullable: false),
                    LocalTeamId = table.Column<int>(type: "int", nullable: false),
                    VisitorTeamId = table.Column<int>(type: "int", nullable: false),
                    Winner = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CupDetails", x => new { x.CupId, x.Round, x.LocalTeamId, x.VisitorTeamId });
                    table.ForeignKey(
                        name: "FK_CupDetails_Cups_CupId",
                        column: x => x.CupId,
                        principalTable: "Cups",
                        principalColumn: "CupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeagueDetails",
                columns: table => new
                {
                    LeagueId = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false),
                    Games = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    For = table.Column<int>(type: "int", nullable: false),
                    Against = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueDetails", x => new { x.LeagueId, x.TeamId });
                    table.ForeignKey(
                        name: "FK_LeagueDetails_Leagues_LeagueId",
                        column: x => x.LeagueId,
                        principalTable: "Leagues",
                        principalColumn: "LeagueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeagueDetails_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "TeamId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeagueDetails_TeamId",
                table: "LeagueDetails",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CupDetails");

            migrationBuilder.DropTable(
                name: "LeagueDetails");

            migrationBuilder.DropTable(
                name: "Cups");

            migrationBuilder.DropTable(
                name: "Leagues");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
