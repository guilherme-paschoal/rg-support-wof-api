﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using RgSupportWofApi.Application.Data;
using System;

namespace RgSupportWofApi.Application.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20171215132548_CreateShiftTableAndAddRelationships")]
    partial class CreateShiftTableAndAddRelationships
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("RgSupportWofApi.Application.Model.Engineer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Engineers");
                });

            modelBuilder.Entity("RgSupportWofApi.Application.Model.Shift", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int?>("EngineerId");

                    b.Property<int>("ShiftOrder");

                    b.HasKey("Id");

                    b.HasIndex("EngineerId");

                    b.ToTable("Shifts");
                });

            modelBuilder.Entity("RgSupportWofApi.Application.Model.Shift", b =>
                {
                    b.HasOne("RgSupportWofApi.Application.Model.Engineer", "Engineer")
                        .WithMany("Shifts")
                        .HasForeignKey("EngineerId");
                });
#pragma warning restore 612, 618
        }
    }
}
