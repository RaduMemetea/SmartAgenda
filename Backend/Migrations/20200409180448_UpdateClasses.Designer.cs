﻿// <auto-generated />
using System;
using BackEnd.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BackEnd.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20200409180448_UpdateClasses")]
    partial class UpdateClasses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataModels.Complex.Conference_Tags", b =>
                {
                    b.Property<int>("ConferenceID")
                        .HasColumnType("int");

                    b.Property<string>("TagID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("ConferenceID", "TagID");

                    b.HasIndex("TagID");

                    b.ToTable("Conference_Tags");
                });

            modelBuilder.Entity("DataModels.Complex.Session_Chair", b =>
                {
                    b.Property<int>("SessionID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("SessionID", "PersonID");

                    b.HasIndex("PersonID");

                    b.ToTable("Session_Chair");
                });

            modelBuilder.Entity("DataModels.Complex.Session_Talks", b =>
                {
                    b.Property<int>("SessionID")
                        .HasColumnType("int");

                    b.Property<int>("TalkID")
                        .HasColumnType("int");

                    b.Property<bool>("Highlight")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("Hour")
                        .HasColumnType("datetime(6)");

                    b.HasKey("SessionID", "TalkID");

                    b.HasIndex("TalkID");

                    b.ToTable("Session_Talks");
                });

            modelBuilder.Entity("DataModels.Complex.Talk_Persons", b =>
                {
                    b.Property<int>("TalkID")
                        .HasColumnType("int");

                    b.Property<int>("PersonID")
                        .HasColumnType("int");

                    b.HasKey("TalkID", "PersonID");

                    b.HasIndex("PersonID");

                    b.ToTable("Talk_Persons");
                });

            modelBuilder.Entity("DataModels.Conference", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("End_Date")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("Start_Date")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.ToTable("Conference");
                });

            modelBuilder.Entity("DataModels.Location", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("DataModels.Person", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("First_Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Last_Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("DataModels.Session", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ConferenceID")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("End_Hour")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("LocationID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTimeOffset>("Start_Hour")
                        .HasColumnType("datetime(6)");

                    b.HasKey("ID");

                    b.HasIndex("ConferenceID");

                    b.HasIndex("LocationID");

                    b.ToTable("Session");
                });

            modelBuilder.Entity("DataModels.Tag", b =>
                {
                    b.Property<string>("ID")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("DataModels.Talk", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Abstract")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("ID");

                    b.ToTable("Talk");
                });

            modelBuilder.Entity("DataModels.Complex.Conference_Tags", b =>
                {
                    b.HasOne("DataModels.Conference", null)
                        .WithMany()
                        .HasForeignKey("ConferenceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModels.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModels.Complex.Session_Chair", b =>
                {
                    b.HasOne("DataModels.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModels.Session", null)
                        .WithMany()
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModels.Complex.Session_Talks", b =>
                {
                    b.HasOne("DataModels.Session", null)
                        .WithMany()
                        .HasForeignKey("SessionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModels.Talk", null)
                        .WithMany()
                        .HasForeignKey("TalkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModels.Complex.Talk_Persons", b =>
                {
                    b.HasOne("DataModels.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModels.Talk", null)
                        .WithMany()
                        .HasForeignKey("TalkID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataModels.Session", b =>
                {
                    b.HasOne("DataModels.Conference", null)
                        .WithMany()
                        .HasForeignKey("ConferenceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataModels.Location", null)
                        .WithMany()
                        .HasForeignKey("LocationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}