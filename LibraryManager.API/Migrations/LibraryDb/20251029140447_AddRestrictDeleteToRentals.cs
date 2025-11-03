using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManager.API.Migrations
{
    public partial class AddRestrictDeleteToRentals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rentals_InventoryNumber",
                table: "Rentals",
                column: "InventoryNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ReaderNumber",
                table: "Rentals",
                column: "ReaderNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Books_InventoryNumber",
                table: "Rentals",
                column: "InventoryNumber",
                principalTable: "Books",
                principalColumn: "InventoryNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Readers_ReaderNumber",
                table: "Rentals",
                column: "ReaderNumber",
                principalTable: "Readers",
                principalColumn: "ReaderNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Books_InventoryNumber",
                table: "Rentals");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Readers_ReaderNumber",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_InventoryNumber",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ReaderNumber",
                table: "Rentals");
        }
    }
}
