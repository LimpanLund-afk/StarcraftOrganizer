using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarcraftOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class PlayerProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FavouredStrat",
                table: "Players",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PlayerBio",
                table: "Players",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FavouredStrat",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "PlayerBio",
                table: "Players");
        }
    }
}
