using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarcraftOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class ANuStart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Race = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Challenges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1Id = table.Column<int>(type: "int", nullable: false),
                    Player2Id = table.Column<int>(type: "int", nullable: false),
                    SeriesFormat = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Player1VetoMapId = table.Column<int>(type: "int", nullable: true),
                    Player2VetoMapId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Challenges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Challenges_Maps_Player1VetoMapId",
                        column: x => x.Player1VetoMapId,
                        principalTable: "Maps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Challenges_Maps_Player2VetoMapId",
                        column: x => x.Player2VetoMapId,
                        principalTable: "Maps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Challenges_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Challenges_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChallengeMap",
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Player1Id = table.Column<int>(type: "int", nullable: false),
                    Player2Id = table.Column<int>(type: "int", nullable: false),
                    MapId = table.Column<int>(type: "int", nullable: false),
                    Player1Won = table.Column<bool>(type: "bit", nullable: false),
                    ChallengeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Challenges_ChallengeId",
                        column: x => x.ChallengeId,
                        principalTable: "Challenges",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matches_Players_Player1Id",
                        column: x => x.Player1Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_Players_Player2Id",
                        column: x => x.Player2Id,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChallengeMaps_MapId",
                table: "ChallengeMap",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_Player1Id",
                table: "Challenges",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_Player1VetoMapId",
                table: "Challenges",
                column: "Player1VetoMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_Player2Id",
                table: "Challenges",
                column: "Player2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Challenges_Player2VetoMapId",
                table: "Challenges",
                column: "Player2VetoMapId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ChallengeId",
                table: "Matches",
                column: "ChallengeId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_MapId",
                table: "Matches",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player1Id",
                table: "Matches",
                column: "Player1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_Player2Id",
                table: "Matches",
                column: "Player2Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChallengeMap");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Challenges");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
