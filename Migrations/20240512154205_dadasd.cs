using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication15.Migrations
{
    public partial class dadasd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Kategoriler",
                columns: table => new
                {
                    KategoriId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriAd = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Kategoriler", x => x.KategoriId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Urun",
                columns: table => new
                {
                    UrunId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KategoriId = table.Column<int>(type: "int", nullable: true),
                    UrunAd = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    UrunFiyat = table.Column<decimal>(type: "money", nullable: true),
                    UrunAdet = table.Column<short>(type: "smallint", nullable: true),
                    UrunPhoto = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Urun", x => x.UrunId);
                    table.ForeignKey(
                        name: "FK_Tbl_Urun_Tbl_Kategoriler_KategoriId",
                        column: x => x.KategoriId,
                        principalTable: "Tbl_Kategoriler",
                        principalColumn: "KategoriId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Urun_KategoriId",
                table: "Tbl_Urun",
                column: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Urun");

            migrationBuilder.DropTable(
                name: "Tbl_Kategoriler");
        }
    }
}
