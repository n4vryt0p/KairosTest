using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KairosTest.Data.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Buku",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JudulBuku = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Pengarang = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    JenisBuku = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    HargaSewa = table.Column<decimal>(type: "decimal(9,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buku", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SewaBuku",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MulaiSewa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SelesaiSewa = table.Column<DateTime>(type: "datetime2", nullable: false),
                    JumlahHari = table.Column<int>(type: "int", nullable: false, computedColumnSql: "DATEDIFF(day, [MulaiSewa], [SelesaiSewa])", stored: true),
                    BukuId = table.Column<int>(type: "int", nullable: false),
                    IdentityUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SewaBuku", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SewaBuku_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SewaBuku_Buku_BukuId",
                        column: x => x.BukuId,
                        principalTable: "Buku",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SewaBuku_BukuId",
                table: "SewaBuku",
                column: "BukuId");

            migrationBuilder.CreateIndex(
                name: "IX_SewaBuku_IdentityUserId",
                table: "SewaBuku",
                column: "IdentityUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SewaBuku");

            migrationBuilder.DropTable(
                name: "Buku");
        }
    }
}
