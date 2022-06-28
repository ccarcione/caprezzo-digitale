using Microsoft.EntityFrameworkCore.Migrations;

namespace CaprezzoDigitale.WebApi.Migrations
{
    public partial class updatefieldcopertinamessaggio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImmagine",
                table: "Messaggi");

            migrationBuilder.AddColumn<string>(
                name: "UrlImmagineCopertina",
                table: "Messaggi",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UrlPdfImmagineCopertina",
                table: "Messaggi",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImmagineCopertina",
                table: "Messaggi");

            migrationBuilder.DropColumn(
                name: "UrlPdfImmagineCopertina",
                table: "Messaggi");

            migrationBuilder.AddColumn<string>(
                name: "UrlImmagine",
                table: "Messaggi",
                type: "text",
                nullable: true);
        }
    }
}
