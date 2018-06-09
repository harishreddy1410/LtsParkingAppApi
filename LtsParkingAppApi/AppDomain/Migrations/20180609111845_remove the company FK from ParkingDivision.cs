using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AppDomain.Migrations
{
    public partial class removethecompanyFKfromParkingDivision : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingDivision_Company_CompanyId",
                table: "ParkingDivision");

            migrationBuilder.DropIndex(
                name: "IX_ParkingDivision_CompanyId",
                table: "ParkingDivision");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ParkingDivision");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ParkingDivision",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingDivision_CompanyId",
                table: "ParkingDivision",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingDivision_Company_CompanyId",
                table: "ParkingDivision",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
