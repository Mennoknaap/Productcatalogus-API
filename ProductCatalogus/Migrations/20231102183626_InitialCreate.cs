using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalogus.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Code = table.Column<string>(type: "TEXT", nullable: false),
                    Naam = table.Column<string>(type: "TEXT", nullable: false),
                    PotMaat = table.Column<int>(type: "INTEGER", nullable: false),
                    PlantHoogte = table.Column<int>(type: "INTEGER", nullable: false),
                    Kleur = table.Column<string>(type: "TEXT", nullable: false),
                    ProductGroep = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Code);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
