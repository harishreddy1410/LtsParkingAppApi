﻿// <auto-generated />
using AppDomain.Contexts;
using AppDomain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace AppDomain.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20180608103909_fixRelationShipIssueInParkingTrafficTable")]
    partial class fixRelationShipIssueInParkingTrafficTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppDomain.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("AppDomain.Models.EmployeeShift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<DateTime>("ShiftBeginTime");

                    b.Property<DateTime>("ShiftEndTime");

                    b.Property<string>("ShiftName");

                    b.HasKey("Id");

                    b.ToTable("EmployeeShift");
                });

            modelBuilder.Entity("AppDomain.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name")
                        .HasMaxLength(30);

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("AppDomain.Models.ParkingDivision", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int>("LocationId");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("SequenceOrder");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("ParkingDivision");
                });

            modelBuilder.Entity("AppDomain.Models.ParkingSlot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsOccupied");

                    b.Property<short>("Level");

                    b.Property<string>("Location");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<int>("ParkingDivisionId");

                    b.Property<int>("SequenceOrder");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("ParkingDivisionId");

                    b.ToTable("ParkingSlot");
                });

            modelBuilder.Entity("AppDomain.Models.ParkingTraffic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<DateTime>("InTime");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<DateTime>("OutTime");

                    b.Property<int>("ParkingSlotId");

                    b.Property<int>("UserProfileId");

                    b.Property<int>("VehicleId");

                    b.HasKey("Id");

                    b.HasIndex("ParkingSlotId");

                    b.HasIndex("UserProfileId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ParkingTraffic");
                });

            modelBuilder.Entity("AppDomain.Models.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CompanyId");

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(100);

                    b.Property<int>("EmployeeShiftId");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("LastName")
                        .HasMaxLength(100);

                    b.Property<int?>("LocationId");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("PreferredName")
                        .HasMaxLength(100);

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("EmployeeShiftId")
                        .IsUnique();

                    b.HasIndex("LocationId");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("AppDomain.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("CreatedDate");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("ModifiedBy");

                    b.Property<DateTime?>("ModifiedDate");

                    b.Property<string>("RegNumber");

                    b.Property<string>("Type");

                    b.Property<int>("UserProfileId");

                    b.HasKey("Id");

                    b.HasIndex("UserProfileId");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("AppDomain.Models.ParkingDivision", b =>
                {
                    b.HasOne("AppDomain.Models.Location")
                        .WithMany("ParkingDivisions")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppDomain.Models.ParkingSlot", b =>
                {
                    b.HasOne("AppDomain.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDomain.Models.ParkingDivision")
                        .WithMany("ParkingSlots")
                        .HasForeignKey("ParkingDivisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppDomain.Models.ParkingTraffic", b =>
                {
                    b.HasOne("AppDomain.Models.ParkingSlot", "ParkingSlot")
                        .WithMany()
                        .HasForeignKey("ParkingSlotId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDomain.Models.UserProfile", "UserProfile")
                        .WithMany("ParkingTraffic")
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDomain.Models.Vehicle", "Vehicle")
                        .WithMany("ParkingTraffic")
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AppDomain.Models.UserProfile", b =>
                {
                    b.HasOne("AppDomain.Models.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDomain.Models.EmployeeShift", "EmployeeShift")
                        .WithOne("UserProfile")
                        .HasForeignKey("AppDomain.Models.UserProfile", "EmployeeShiftId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AppDomain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("AppDomain.Models.Vehicle", b =>
                {
                    b.HasOne("AppDomain.Models.UserProfile", "UserProfile")
                        .WithMany()
                        .HasForeignKey("UserProfileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
