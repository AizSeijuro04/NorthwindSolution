using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Persistence.Migrations.Northwind
{
    public partial class AddRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99bae8ea-54e1-4231-b3cf-7a97018ef896", "c0844b5d-0ce9-4bce-bbc8-9c96f76e93cb", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "44b96b1b-8365-47af-963b-4a4c4c9e1ded", "ad8609d6-6a5e-432d-8cff-19ff50fd0528", "Administration", " ADMINISTRATION" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44b96b1b-8365-47af-963b-4a4c4c9e1ded");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99bae8ea-54e1-4231-b3cf-7a97018ef896");
        }
    }
}
