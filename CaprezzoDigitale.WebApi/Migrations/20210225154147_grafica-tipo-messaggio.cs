using Microsoft.EntityFrameworkCore.Migrations;

namespace CaprezzoDigitale.WebApi.Migrations
{
    public partial class graficatipomessaggio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Colore",
                table: "TipiMessaggio",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icona",
                table: "TipiMessaggio",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colore",
                table: "TipiMessaggio");

            migrationBuilder.DropColumn(
                name: "Icona",
                table: "TipiMessaggio");
        }
    }
}
