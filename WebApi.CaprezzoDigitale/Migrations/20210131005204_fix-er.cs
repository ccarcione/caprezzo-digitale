using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.CaprezzoDigitale.Migrations
{
    public partial class fixer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allegati_Messaggi_MessaggioId",
                table: "Allegati");

            migrationBuilder.DropForeignKey(
                name: "FK_Messaggi_TipiMessaggio_TipoMessaggioId",
                table: "Messaggi");

            migrationBuilder.AddForeignKey(
                name: "FK_Allegati_Messaggi_MessaggioId",
                table: "Allegati",
                column: "MessaggioId",
                principalTable: "Messaggi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messaggi_TipiMessaggio_TipoMessaggioId",
                table: "Messaggi",
                column: "TipoMessaggioId",
                principalTable: "TipiMessaggio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allegati_Messaggi_MessaggioId",
                table: "Allegati");

            migrationBuilder.DropForeignKey(
                name: "FK_Messaggi_TipiMessaggio_TipoMessaggioId",
                table: "Messaggi");

            migrationBuilder.AddForeignKey(
                name: "FK_Allegati_Messaggi_MessaggioId",
                table: "Allegati",
                column: "MessaggioId",
                principalTable: "Messaggi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messaggi_TipiMessaggio_TipoMessaggioId",
                table: "Messaggi",
                column: "TipoMessaggioId",
                principalTable: "TipiMessaggio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
