using Microsoft.EntityFrameworkCore.Migrations;

namespace web_mvc.Data.Migrations
{
    public partial class FigurineAddImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Figurine",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Figurine");
        }
    }
}
