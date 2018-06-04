using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace AppDomain.Migrations
{
    public partial class restructureDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ParkingSlot",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "ParkingSlot",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ParkingDivisionId",
                table: "ParkingSlot",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ParkingDivision",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    ModifiedBy = table.Column<int>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingDivision", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingDivision_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_CompanyId",
                table: "UserProfile",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlot_CompanyId",
                table: "ParkingSlot",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlot_ParkingDivisionId",
                table: "ParkingSlot",
                column: "ParkingDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingDivision_LocationId",
                table: "ParkingDivision",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSlot_Company_CompanyId",
                table: "ParkingSlot",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSlot_ParkingDivision_ParkingDivisionId",
                table: "ParkingSlot",
                column: "ParkingDivisionId",
                principalTable: "ParkingDivision",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Company_CompanyId",
                table: "UserProfile",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSlot_Company_CompanyId",
                table: "ParkingSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSlot_ParkingDivision_ParkingDivisionId",
                table: "ParkingSlot");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Company_CompanyId",
                table: "UserProfile");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "ParkingDivision");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_CompanyId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSlot_CompanyId",
                table: "ParkingSlot");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSlot_ParkingDivisionId",
                table: "ParkingSlot");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "ParkingSlot");

            migrationBuilder.DropColumn(
                name: "ParkingDivisionId",
                table: "ParkingSlot");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ParkingSlot",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
