using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AppDomain.Migrations
{
    public partial class addingLocationFKtoCompanyandComapnyFKtoParkingDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ParkingDivision",
                nullable: true,
                defaultValue: null);

            migrationBuilder.AddColumn<int>(
                name: "ParkingLocationId",
                table: "Company",
                nullable: true,
                defaultValue: null);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingDivision_CompanyId",
                table: "ParkingDivision",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ParkingLocationId",
                table: "Company",
                column: "ParkingLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Company_Location_ParkingLocationId",
                table: "Company",
                column: "ParkingLocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingDivision_Company_CompanyId",
                table: "ParkingDivision",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Company_Location_ParkingLocationId",
                table: "Company");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingDivision_Company_CompanyId",
                table: "ParkingDivision");

            migrationBuilder.DropIndex(
                name: "IX_ParkingDivision_CompanyId",
                table: "ParkingDivision");

            migrationBuilder.DropIndex(
                name: "IX_Company_ParkingLocationId",
                table: "Company");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ParkingDivision");

            migrationBuilder.DropColumn(
                name: "ParkingLocationId",
                table: "Company");
        }
    }
}
