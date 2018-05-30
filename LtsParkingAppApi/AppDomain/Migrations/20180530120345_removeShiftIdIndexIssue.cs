using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AppDomain.Migrations
{
    public partial class removeShiftIdIndexIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_EmployeeShift_EmployeeShiftId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_EmployeeShiftId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "EmployeeShiftId",
                table: "UserProfile");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserProfile_Id",
            //    table: "UserProfile",
            //    column: "Id",
            //    unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_EmployeeShift_Id",
                table: "UserProfile",
                column: "Id",
                principalTable: "EmployeeShift",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_EmployeeShift_Id",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_Id",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeShiftId",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserProfile_EmployeeShiftId",
            //    table: "UserProfile",
            //    column: "EmployeeShiftId",
            //    unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_EmployeeShift_EmployeeShiftId",
                table: "UserProfile",
                column: "EmployeeShiftId",
                principalTable: "EmployeeShift",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
