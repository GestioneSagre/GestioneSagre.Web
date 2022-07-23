using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestioneSagre.Web.Migrations.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaVideo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaStampa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuidFesta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Festa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInizio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataFine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuidFesta = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusFesta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Festa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Versione",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceVersione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestoVersione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VersioneStato = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versione", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prodotto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriaId = table.Column<int>(type: "int", nullable: false),
                    Prodotto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProdottoAttivo = table.Column<bool>(type: "bit", nullable: false),
                    Prezzo_Amount = table.Column<float>(type: "real", nullable: true),
                    Prezzo_Currency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantita = table.Column<int>(type: "int", nullable: false),
                    QuantitaAttiva = table.Column<bool>(type: "bit", nullable: false),
                    QuantitaScorta = table.Column<int>(type: "int", nullable: false),
                    AvvisoScorta = table.Column<bool>(type: "bit", nullable: false),
                    Prenotazione = table.Column<bool>(type: "bit", nullable: false),
                    GuidFesta = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prodotto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prodotto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Intestazione",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FestaId = table.Column<int>(type: "int", nullable: false),
                    Titolo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Edizione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Luogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Logo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Intestazione", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Intestazione_Festa_FestaId",
                        column: x => x.FestaId,
                        principalTable: "Festa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Intestazione_FestaId",
                table: "Intestazione",
                column: "FestaId");

            migrationBuilder.CreateIndex(
                name: "IX_Prodotto_CategoriaId",
                table: "Prodotto",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Intestazione");

            migrationBuilder.DropTable(
                name: "Prodotto");

            migrationBuilder.DropTable(
                name: "Versione");

            migrationBuilder.DropTable(
                name: "Festa");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
