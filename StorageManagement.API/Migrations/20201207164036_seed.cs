using Microsoft.EntityFrameworkCore.Migrations;

namespace StorageManagement.API.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "City", "Name", "PostCode", "Street", "UnitNumber" },
                values: new object[] { 1, "Rzeszów", "Magazyn 1", "35-311", "Eugeniusza Kwiatkowskiego", "45" });

            migrationBuilder.InsertData(
                table: "StorageRacks",
                columns: new[] { "Id", "ContractorId", "IsTaken", "RackNumber", "WarehouseId" },
                values: new object[,]
                {
                    { 1, null, false, "A1", 1 },
                    { 2, null, false, "A2", 1 },
                    { 3, null, false, "B1", 1 },
                    { 4, null, false, "B2", 1 },
                    { 5, null, false, "C1", 1 }
                });

            migrationBuilder.InsertData(
                table: "Shelves",
                columns: new[] { "Id", "ProductId", "Quantity", "RackId", "ShelfNumber" },
                values: new object[,]
                {
                    { 1, null, 0, 1, "1" },
                    { 18, null, 0, 5, "2" },
                    { 17, null, 0, 5, "1" },
                    { 16, null, 0, 4, "4" },
                    { 15, null, 0, 4, "3" },
                    { 14, null, 0, 4, "2" },
                    { 13, null, 0, 4, "1" },
                    { 12, null, 0, 3, "4" },
                    { 11, null, 0, 3, "3" },
                    { 10, null, 0, 3, "2" },
                    { 9, null, 0, 3, "1" },
                    { 8, null, 0, 2, "4" },
                    { 7, null, 0, 2, "3" },
                    { 6, null, 0, 2, "2" },
                    { 5, null, 0, 2, "1" },
                    { 4, null, 0, 1, "4" },
                    { 3, null, 0, 1, "3" },
                    { 2, null, 0, 1, "2" },
                    { 19, null, 0, 5, "3" },
                    { 20, null, 0, 5, "4" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Shelves",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "StorageRacks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StorageRacks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StorageRacks",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StorageRacks",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StorageRacks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
