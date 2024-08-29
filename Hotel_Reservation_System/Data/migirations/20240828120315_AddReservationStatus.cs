using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_Reservation_System.Data.migirations
{
    /// <inheritdoc />
    public partial class AddReservationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilitiesId",
                table: "RoomFacilities");

            migrationBuilder.RenameColumn(
                name: "FacilitiesId",
                table: "RoomFacilities",
                newName: "FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomFacilities_FacilitiesId",
                table: "RoomFacilities",
                newName: "IX_RoomFacilities_FacilityId");

            migrationBuilder.AlterColumn<string>(
                name: "ReservationStatus",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityId",
                table: "RoomFacilities",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilityId",
                table: "RoomFacilities");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "RoomFacilities",
                newName: "FacilitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomFacilities_FacilityId",
                table: "RoomFacilities",
                newName: "IX_RoomFacilities_FacilitiesId");

            migrationBuilder.AlterColumn<int>(
                name: "ReservationStatus",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomFacilities_Facilities_FacilitiesId",
                table: "RoomFacilities",
                column: "FacilitiesId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
