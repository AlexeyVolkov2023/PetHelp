using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "phone_number_value",
                table: "volunteers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "full_name_surname",
                table: "volunteers",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "full_name_patronymic",
                table: "volunteers",
                newName: "patronymic");

            migrationBuilder.RenameColumn(
                name: "full_name_name",
                table: "volunteers",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "experience_in_years_value",
                table: "volunteers",
                newName: "experience_in_years");

            migrationBuilder.RenameColumn(
                name: "email_value",
                table: "volunteers",
                newName: "email");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "volunteers",
                newName: "full_name_surname");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "volunteers",
                newName: "phone_number_value");

            migrationBuilder.RenameColumn(
                name: "patronymic",
                table: "volunteers",
                newName: "full_name_patronymic");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "volunteers",
                newName: "full_name_name");

            migrationBuilder.RenameColumn(
                name: "experience_in_years",
                table: "volunteers",
                newName: "experience_in_years_value");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "volunteers",
                newName: "email_value");
        }
    }
}
