using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarcraftOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class MatchRace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Player1Race",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Player2Race",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Player1Race",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "Player2Race",
                table: "Matches");
        }
    }
}
