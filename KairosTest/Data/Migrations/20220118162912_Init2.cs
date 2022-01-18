using Microsoft.EntityFrameworkCore.Migrations;

namespace KairosTest.Data.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "539491f8-b120-43e3-ac44-9e79661058d0", "c4df3c0d-09aa-4d83-ac89-6f59f10cd188", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0158ccb0-068b-450f-b1f8-0ad09cd38834", "6e4d7a6b-bf77-4e72-a6cb-be82defce359", "Penyewa", "PENYEWA" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0158ccb0-068b-450f-b1f8-0ad09cd38834");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "539491f8-b120-43e3-ac44-9e79661058d0");
        }
    }
}
