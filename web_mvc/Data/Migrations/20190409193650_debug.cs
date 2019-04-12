using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web_mvc.Data.Migrations
{
    public partial class debug : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorie",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    nom = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Figurine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(nullable: true),
                    prix_ttc = table.Column<float>(nullable: false),
                    quantite_magasin = table.Column<int>(nullable: false),
                    quantite_stock = table.Column<int>(nullable: false),
                    date_parution = table.Column<DateTime>(nullable: false),
                    nb_exemplaires = table.Column<int>(nullable: false),
                    poids = table.Column<float>(nullable: false),
                    largeur = table.Column<float>(nullable: false),
                    hauteur = table.Column<float>(nullable: false),
                    longueur = table.Column<float>(nullable: false),
                    reference = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    marqueId = table.Column<int>(nullable: true),
                    categorieId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Figurine", x => x.Id);

                    table.ForeignKey(
                        name: "FK_Figurine_Marque_marqueId",
                        column: x => x.marqueId,
                        principalTable: "Marque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                       name: "FK_Figurine_Categorie_categorieId",
                       column: x => x.categorieId,
                       principalTable: "Categorie",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Figurine_categorieId",
                table: "Figurine",
                column: "categorieId");

            migrationBuilder.CreateIndex(
                name: "IX_Figurine_marqueId",
                table: "Figurine",
                column: "marqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Figurine");

            migrationBuilder.DropTable(
                name: "Categorie");
        }
    }
}