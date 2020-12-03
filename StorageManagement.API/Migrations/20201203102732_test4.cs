using Microsoft.EntityFrameworkCore.Migrations;

namespace StorageManagement.API.Migrations
{
    public partial class test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Products_ProductId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Shelves_ProductId",
                table: "Shelves");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Shelves",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_ProductId",
                table: "Shelves",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Products_ProductId",
                table: "Shelves",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shelves_Products_ProductId",
                table: "Shelves");

            migrationBuilder.DropIndex(
                name: "IX_Shelves_ProductId",
                table: "Shelves");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shelves_ProductId",
                table: "Shelves",
                column: "ProductId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Shelves_Products_ProductId",
                table: "Shelves",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
