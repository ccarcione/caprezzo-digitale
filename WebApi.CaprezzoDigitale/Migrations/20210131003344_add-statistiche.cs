using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace WebApi.CaprezzoDigitale.Migrations
{
    public partial class addstatistiche : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipiStatistica",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(nullable: true),
                    Descritione = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipiStatistica", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistiche",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TipoStatisticaId = table.Column<short>(nullable: false),
                    Valore = table.Column<string>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistiche", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Statistiche_TipiStatistica_TipoStatisticaId",
                        column: x => x.TipoStatisticaId,
                        principalTable: "TipiStatistica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Statistiche_TipoStatisticaId",
                table: "Statistiche",
                column: "TipoStatisticaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Statistiche");

            migrationBuilder.DropTable(
                name: "TipiStatistica");
        }
    }
}
