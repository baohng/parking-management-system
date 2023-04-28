using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingManagementSystem.Data.Migrations
{
    public partial class CreateCheckInOut : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpaces_ParkingSpaceId1",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ParkingSpaceId1",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ParkingSpaceId1",
                table: "Reservations");

            migrationBuilder.AlterColumn<int>(
                name: "ParkingSpaceId",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "CheckInOuts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    ParkingSpaceId = table.Column<int>(type: "int", nullable: false),
                    CheckInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckOutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckInOuts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckInOuts_ParkingSpaces_ParkingSpaceId",
                        column: x => x.ParkingSpaceId,
                        principalTable: "ParkingSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CheckInOuts_Reservations_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ParkingSpaceId",
                table: "Reservations",
                column: "ParkingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckInOuts_ParkingSpaceId",
                table: "CheckInOuts",
                column: "ParkingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckInOuts_ReservationId",
                table: "CheckInOuts",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ParkingSpaces_ParkingSpaceId",
                table: "Reservations",
                column: "ParkingSpaceId",
                principalTable: "ParkingSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_ParkingSpaces_ParkingSpaceId",
                table: "Reservations");

            migrationBuilder.DropTable(
                name: "CheckInOuts");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ParkingSpaceId",
                table: "Reservations");

            migrationBuilder.AlterColumn<string>(
                name: "ParkingSpaceId",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ParkingSpaceId1",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ParkingSpaceId1",
                table: "Reservations",
                column: "ParkingSpaceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_ParkingSpaces_ParkingSpaceId1",
                table: "Reservations",
                column: "ParkingSpaceId1",
                principalTable: "ParkingSpaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
