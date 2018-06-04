using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AppDomain.Migrations
{
    public partial class addinglocationFKtoUserprofiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "UserProfile");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_LocationId",
                table: "UserProfile",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Location_LocationId",
                table: "UserProfile",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Location_LocationId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_LocationId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "UserProfile");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "UserProfile",
                nullable: true);
        }
    }
}
