using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarcraftOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class ADDEDIMGURLTOMAP : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Maps",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Maps",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Maps");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Maps",
                newName: "id");
        }
    }
}
