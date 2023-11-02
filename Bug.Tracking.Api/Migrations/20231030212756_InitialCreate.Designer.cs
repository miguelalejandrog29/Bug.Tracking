﻿// <auto-generated />
using System;
using Bug.Tracking.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bug.Tracking.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231030212756_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bug.Tracking.Api.Data.Entity.Project", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Description = "sdfhkjdghdkfhgkdjf",
                            Name = "Compact"
                        },
                        new
                        {
                            Id = 2L,
                            Description = "etertbewt etrwtrwtwetwe wetwertw etwert",
                            Name = "Fritz"
                        },
                        new
                        {
                            Id = 3L,
                            Description = "Cha4356456b 456453b56bw4srtwecon",
                            Name = "4una"
                        },
                        new
                        {
                            Id = 4L,
                            Description = "563b456bery y tyertyertyhf ggh dfghuye",
                            Name = "Diler"
                        },
                        new
                        {
                            Id = 5L,
                            Description = "4564 rtgwtywey wage rte dfd fgdfgertewrwer werwerwerwerfsdfsdlez",
                            Name = "Brench"
                        });
                });

            modelBuilder.Entity("Bug.Tracking.Api.Data.Entity.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Federico",
                            Surname = "Guerra"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Frank",
                            Surname = "Montalvo"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Adriel",
                            Surname = "Chacon"
                        },
                        new
                        {
                            Id = 4L,
                            Name = "Ana",
                            Surname = "Olivera"
                        },
                        new
                        {
                            Id = 5L,
                            Name = "Guillen",
                            Surname = "Gonzalez"
                        });
                });

            modelBuilder.Entity("Bug.Tracking.Api.Data.Entity.UserBug", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<long>("ProjectId")
                        .HasColumnType("bigint");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId");

                    b.ToTable("UserBugs");
                });

            modelBuilder.Entity("Bug.Tracking.Api.Data.Entity.UserBug", b =>
                {
                    b.HasOne("Bug.Tracking.Api.Data.Entity.Project", "Project")
                        .WithMany("UserBugs")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Bug.Tracking.Api.Data.Entity.User", "User")
                        .WithMany("UserBugs")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Bug.Tracking.Api.Data.Entity.Project", b =>
                {
                    b.Navigation("UserBugs");
                });

            modelBuilder.Entity("Bug.Tracking.Api.Data.Entity.User", b =>
                {
                    b.Navigation("UserBugs");
                });
#pragma warning restore 612, 618
        }
    }
}
