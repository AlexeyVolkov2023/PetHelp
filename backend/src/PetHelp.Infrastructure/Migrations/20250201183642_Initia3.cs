using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetHelp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initia3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "address",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "networks",
                table: "volunteers",
                newName: "social+networks");

            migrationBuilder.RenameColumn(
                name: "details",
                table: "volunteers",
                newName: "payment_details");

            migrationBuilder.RenameColumn(
                name: "status_value",
                table: "pets",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "details",
                table: "pets",
                newName: "payment_details");

            migrationBuilder.RenameColumn(
                name: "species_breed_species_id",
                table: "pets",
                newName: "species_id");

            migrationBuilder.RenameColumn(
                name: "species_breed_breed_id",
                table: "pets",
                newName: "breed_id");

            migrationBuilder.RenameColumn(
                name: "height",
                table: "pets",
                newName: "Weight");

            migrationBuilder.AlterColumn<string>(
                name: "health_info",
                table: "pets",
                type: "character varying(250)",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<int>(
                name: "apartment",
                table: "pets",
                type: "integer",
                maxLength: 1600,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "pets",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "country",
                table: "pets",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "house_number",
                table: "pets",
                type: "character varying(6)",
                maxLength: 6,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "region",
                table: "pets",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "street",
                table: "pets",
                type: "character varying(35)",
                maxLength: 35,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apartment",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "city",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "country",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "house_number",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "region",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "street",
                table: "pets");

            migrationBuilder.RenameColumn(
                name: "social+networks",
                table: "volunteers",
                newName: "networks");

            migrationBuilder.RenameColumn(
                name: "payment_details",
                table: "volunteers",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "pets",
                newName: "status_value");

            migrationBuilder.RenameColumn(
                name: "payment_details",
                table: "pets",
                newName: "details");

            migrationBuilder.RenameColumn(
                name: "species_id",
                table: "pets",
                newName: "species_breed_species_id");

            migrationBuilder.RenameColumn(
                name: "breed_id",
                table: "pets",
                newName: "species_breed_breed_id");

            migrationBuilder.RenameColumn(
                name: "Weight",
                table: "pets",
                newName: "height");

            migrationBuilder.AlterColumn<string>(
                name: "health_info",
                table: "pets",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(250)",
                oldMaxLength: 250);

            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "pets",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");
        }
    }
}
