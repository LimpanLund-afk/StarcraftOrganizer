using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarcraftOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class ChallengeMapsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Maps_Challenges_ChallengeId",
                table: "Maps");

            migrationBuilder.DropIndex(
                name: "IX_Maps_ChallengeId",
                table: "Maps");

            migrationBuilder.DropColumn(
                name: "ChallengeId",
                table: "Maps");

            migrationBuilder.CreateTable(
                name: "ChallengeMaps",
                columns: table => new
                {
                    ChallengeId = table.Column<int>(type: "int", nullable: false),
                    MapId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChallengeMaps", x => new { x.ChallengeId, x.MapId });
                    table.ForeignKey(
                        name: "FK_ChallengeMaps_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChallengeMaps_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeMaps_MapId",
                table: "ChallengeMaps",
                column: "MapId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeMaps");

            migrationBuilder.AddColumn<int>(
                name: "ChallengeId",
                table: "Maps",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Maps_ChallengeId",
                table: "Maps",
                column: "ChallengeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Maps_Challenges_ChallengeId",
                table: "Maps",
                column: "ChallengeId",
                principalTable: "Challenges",
                principalColumn: "Id");
        }
    }
}
