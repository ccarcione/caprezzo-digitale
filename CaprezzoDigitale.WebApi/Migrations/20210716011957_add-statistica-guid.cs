using Microsoft.EntityFrameworkCore.Migrations;

namespace CaprezzoDigitale.WebApi.Migrations
{
    public partial class addstatisticaguid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Guid",
                table: "Statistiche",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Guid",
                table: "Statistiche");
        }
    }
}
