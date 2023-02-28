using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DroneManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStorageFileDataIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medicine_StorageFileDataId",
                schema: "DroneManager",
                table: "Medicine");

            migrationBuilder.AlterColumn<int>(
                name: "StorageFileDataId",
                schema: "DroneManager",
                table: "Medicine",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_StorageFileDataId",
                schema: "DroneManager",
                table: "Medicine",
                column: "StorageFileDataId",
                unique: true,
                filter: "[StorageFileDataId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medicine_StorageFileDataId",
                schema: "DroneManager",
                table: "Medicine");

            migrationBuilder.AlterColumn<int>(
                name: "StorageFileDataId",
                schema: "DroneManager",
                table: "Medicine",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_StorageFileDataId",
                schema: "DroneManager",
                table: "Medicine",
                column: "StorageFileDataId",
                unique: true);
        }
    }
}
