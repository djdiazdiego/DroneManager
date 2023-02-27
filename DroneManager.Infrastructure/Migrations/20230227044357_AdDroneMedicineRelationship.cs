using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DroneManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdDroneMedicineRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dron",
                schema: "DroneManager");

            migrationBuilder.DropTable(
                name: "DronModel",
                schema: "DroneManager");

            migrationBuilder.DropTable(
                name: "DronStatus",
                schema: "DroneManager");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                schema: "DroneManager",
                table: "StorageFileData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                schema: "DroneManager",
                table: "StorageFileData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Key",
                schema: "DroneManager",
                table: "StorageFileData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DroneId",
                schema: "DroneManager",
                table: "Medicine",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DroneModel",
                schema: "DroneManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DroneStatus",
                schema: "DroneManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DroneStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drone",
                schema: "DroneManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    BatteryCapacity = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Drone_DroneModel_ModelId",
                        column: x => x.ModelId,
                        principalSchema: "DroneManager",
                        principalTable: "DroneModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Drone_DroneStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "DroneManager",
                        principalTable: "DroneStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medicine_DroneId",
                schema: "DroneManager",
                table: "Medicine",
                column: "DroneId");

            migrationBuilder.CreateIndex(
                name: "IX_Drone_ModelId",
                schema: "DroneManager",
                table: "Drone",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Drone_StatusId",
                schema: "DroneManager",
                table: "Drone",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicine_Drone_DroneId",
                schema: "DroneManager",
                table: "Medicine",
                column: "DroneId",
                principalSchema: "DroneManager",
                principalTable: "Drone",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicine_Drone_DroneId",
                schema: "DroneManager",
                table: "Medicine");

            migrationBuilder.DropTable(
                name: "Drone",
                schema: "DroneManager");

            migrationBuilder.DropTable(
                name: "DroneModel",
                schema: "DroneManager");

            migrationBuilder.DropTable(
                name: "DroneStatus",
                schema: "DroneManager");

            migrationBuilder.DropIndex(
                name: "IX_Medicine_DroneId",
                schema: "DroneManager",
                table: "Medicine");

            migrationBuilder.DropColumn(
                name: "Extension",
                schema: "DroneManager",
                table: "StorageFileData");

            migrationBuilder.DropColumn(
                name: "FileName",
                schema: "DroneManager",
                table: "StorageFileData");

            migrationBuilder.DropColumn(
                name: "Key",
                schema: "DroneManager",
                table: "StorageFileData");

            migrationBuilder.DropColumn(
                name: "DroneId",
                schema: "DroneManager",
                table: "Medicine");

            migrationBuilder.CreateTable(
                name: "DronModel",
                schema: "DroneManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DronModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DronStatus",
                schema: "DroneManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DronStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dron",
                schema: "DroneManager",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    BatteryCapacity = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    SerialNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dron", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dron_DronModel_ModelId",
                        column: x => x.ModelId,
                        principalSchema: "DroneManager",
                        principalTable: "DronModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dron_DronStatus_StatusId",
                        column: x => x.StatusId,
                        principalSchema: "DroneManager",
                        principalTable: "DronStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dron_ModelId",
                schema: "DroneManager",
                table: "Dron",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Dron_StatusId",
                schema: "DroneManager",
                table: "Dron",
                column: "StatusId");
        }
    }
}
