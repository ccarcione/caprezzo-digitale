using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CaprezzoDigitale.WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipiMessaggio",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DisplayName = table.Column<string>(nullable: true),
                    Descrizione = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipiMessaggio", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messaggi",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titolo = table.Column<string>(nullable: true),
                    Sottotitolo = table.Column<string>(nullable: true),
                    DataPubblicazione = table.Column<DateTime>(nullable: false),
                    UrlImmagine = table.Column<string>(nullable: true),
                    Testo = table.Column<string>(nullable: true),
                    TipoMessaggioId = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messaggi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messaggi_TipiMessaggio_TipoMessaggioId",
                        column: x => x.TipoMessaggioId,
                        principalTable: "TipiMessaggio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allegati",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessaggioId = table.Column<long>(nullable: false),
                    Descrizione = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allegati", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Allegati_Messaggi_MessaggioId",
                        column: x => x.MessaggioId,
                        principalTable: "Messaggi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allegati_MessaggioId",
                table: "Allegati",
                column: "MessaggioId");

            migrationBuilder.CreateIndex(
                name: "IX_Messaggi_TipoMessaggioId",
                table: "Messaggi",
                column: "TipoMessaggioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allegati");

            migrationBuilder.DropTable(
                name: "Messaggi");

            migrationBuilder.DropTable(
                name: "TipiMessaggio");
        }
    }
}
