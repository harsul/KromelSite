using Microsoft.EntityFrameworkCore.Migrations;

namespace KromelSite.Data.Migrations
{
    public partial class makina : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Makina",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MakinaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MakinaTanitim = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrunGruplariID = table.Column<int>(type: "int", nullable: false),
                    ResimYolu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Makina", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Makina_UrunGruplari_UrunGruplariID",
                        column: x => x.UrunGruplariID,
                        principalTable: "UrunGruplari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Makina_UrunGruplariID",
                table: "Makina",
                column: "UrunGruplariID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Makina");
        }
    }
}
