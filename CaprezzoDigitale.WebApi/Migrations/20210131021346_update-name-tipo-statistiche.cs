using Microsoft.EntityFrameworkCore.Migrations;

namespace CaprezzoDigitale.WebApi.Migrations
{
    public partial class updatenametipostatistiche : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descritione",
                table: "TipiStatistica");

            migrationBuilder.AddColumn<string>(
                name: "Descrizione",
                table: "TipiStatistica",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descrizione",
                table: "TipiStatistica");

            migrationBuilder.AddColumn<string>(
                name: "Descritione",
                table: "TipiStatistica",
                type: "text",
                nullable: true);
        }
    }
}
