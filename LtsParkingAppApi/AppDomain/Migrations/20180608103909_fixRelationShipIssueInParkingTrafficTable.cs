using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AppDomain.Migrations
{
    public partial class fixRelationShipIssueInParkingTrafficTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParkingTraffic_UserProfileId",
                table: "ParkingTraffic");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTraffic_VehicleId",
                table: "ParkingTraffic");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTraffic_UserProfileId",
                table: "ParkingTraffic",
                column: "UserProfileId",
                unique:false);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTraffic_VehicleId",
                table: "ParkingTraffic",
                column: "VehicleId",
                unique:false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ParkingTraffic_UserProfileId",
                table: "ParkingTraffic");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTraffic_VehicleId",
                table: "ParkingTraffic");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTraffic_UserProfileId",
                table: "ParkingTraffic",
                column: "UserProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTraffic_VehicleId",
                table: "ParkingTraffic",
                column: "VehicleId",
                unique: true);
        }
    }
}
