using Microsoft.EntityFrameworkCore.Migrations;

namespace StorageManagement.API.Migrations
{
    public partial class testmig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageRacks_Warehouses_WarehouseId",
                table: "StorageRacks");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "StorageRacks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StorageRacks_Warehouses_WarehouseId",
                table: "StorageRacks",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorageRacks_Warehouses_WarehouseId",
                table: "StorageRacks");

            migrationBuilder.AlterColumn<int>(
                name: "WarehouseId",
                table: "StorageRacks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_StorageRacks_Warehouses_WarehouseId",
                table: "StorageRacks",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
