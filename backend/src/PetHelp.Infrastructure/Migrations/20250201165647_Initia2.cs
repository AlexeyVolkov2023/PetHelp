using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initia2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description_value",
                table: "volunteers",
                newName: "description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "description",
                table: "volunteers",
                newName: "description_value");
        }
    }
}
