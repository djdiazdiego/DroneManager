using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DroneManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddDron : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DroneManager");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
