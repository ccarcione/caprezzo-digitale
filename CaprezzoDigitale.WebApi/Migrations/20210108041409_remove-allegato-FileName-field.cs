using Microsoft.EntityFrameworkCore.Migrations;

namespace CaprezzoDigitale.WebApi.Migrations
{
    public partial class removeallegatoFileNamefield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Allegati");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Allegati",
                type: "text",
                nullable: true);
        }
    }
}
