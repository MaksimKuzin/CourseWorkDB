﻿// <auto-generated />
using System;
using CourseWorkDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CourseWorkDB.Migrations
{
    [DbContext(typeof(ChurchParishCourseWorkContext))]
    [Migration("20230107144006_A")]
    partial class A
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CourseWorkDB.DataBase.Activity", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<short?>("Duration")
                        .HasColumnType("smallint");

                    b.Property<short?>("ParishionerAmount")
                        .HasColumnType("smallint");

                    b.HasKey("EventId");

                    b.ToTable("Activity", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.DivineService", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Justification")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Prayer")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EventId");

                    b.ToTable("DivineService", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ParishionerId")
                        .HasColumnType("int");

                    b.Property<string>("Purpose")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Sum")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("ParishionerId");

                    b.ToTable("Donation", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("PriestId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PriestId");

                    b.ToTable("Event", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Inventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfPurchase")
                        .HasColumnType("date");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("Inventory", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Parishioner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<short>("Age")
                        .HasColumnType("smallint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<int>("PriestId")
                        .HasColumnType("int");

                    b.Property<bool>("Sex")
                        .HasColumnType("bit");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("PriestId");

                    b.ToTable("Parishioner", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Priest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("InitialDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Priest", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.SacredEvent", b =>
                {
                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Place")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Route")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("SourceOfFunding")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Transport")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("EventId");

                    b.ToTable("SacredEvent", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ParishionerEvent", b =>
                {
                    b.Property<int>("ParishionerId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.HasKey("ParishionerId", "EventId");

                    b.HasIndex("EventId");

                    b.ToTable("ParishionerEvent", (string)null);
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Activity", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Event", "Event")
                        .WithOne("Activity")
                        .HasForeignKey("CourseWorkDB.DataBase.Activity", "EventId")
                        .IsRequired()
                        .HasConstraintName("FK_Activity_Event");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.DivineService", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Event", "Event")
                        .WithOne("DivineService")
                        .HasForeignKey("CourseWorkDB.DataBase.DivineService", "EventId")
                        .IsRequired()
                        .HasConstraintName("FK_DivineService_Event");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Donation", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Parishioner", "Parishioner")
                        .WithMany("Donations")
                        .HasForeignKey("ParishionerId")
                        .IsRequired()
                        .HasConstraintName("FK_Donation_Parishioner");

                    b.Navigation("Parishioner");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Event", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Priest", "Priest")
                        .WithMany("Events")
                        .HasForeignKey("PriestId")
                        .IsRequired()
                        .HasConstraintName("FK_Event_Priest");

                    b.Navigation("Priest");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Inventory", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Event", "Event")
                        .WithMany("Inventories")
                        .HasForeignKey("EventId")
                        .IsRequired()
                        .HasConstraintName("FK_Inventory_Event");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Parishioner", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Priest", "Priest")
                        .WithMany("Parishioners")
                        .HasForeignKey("PriestId")
                        .IsRequired()
                        .HasConstraintName("FK_Parishioner_Priest");

                    b.Navigation("Priest");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.SacredEvent", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Event", "Event")
                        .WithOne("SacredEvent")
                        .HasForeignKey("CourseWorkDB.DataBase.SacredEvent", "EventId")
                        .IsRequired()
                        .HasConstraintName("FK_SacredEvent_Event");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("ParishionerEvent", b =>
                {
                    b.HasOne("CourseWorkDB.DataBase.Event", null)
                        .WithMany()
                        .HasForeignKey("EventId")
                        .IsRequired()
                        .HasConstraintName("FK_ParishionerEvent_Event");

                    b.HasOne("CourseWorkDB.DataBase.Parishioner", null)
                        .WithMany()
                        .HasForeignKey("ParishionerId")
                        .IsRequired()
                        .HasConstraintName("FK_ParishionerEvent_Parishioner");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Event", b =>
                {
                    b.Navigation("Activity");

                    b.Navigation("DivineService");

                    b.Navigation("Inventories");

                    b.Navigation("SacredEvent");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Parishioner", b =>
                {
                    b.Navigation("Donations");
                });

            modelBuilder.Entity("CourseWorkDB.DataBase.Priest", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("Parishioners");
                });
#pragma warning restore 612, 618
        }
    }
}
