﻿// <auto-generated />
using System;
using Exam.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Exam.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Exam.Domain.Entities.CategoryEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Alpha")
                        .HasColumnType("text");

                    b.Property<string>("Flag")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SystemId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Categories", "public");
                });

            modelBuilder.Entity("Exam.Domain.Entities.LeagueEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SystemId")
                        .HasColumnType("bigint");

                    b.Property<int>("UserCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Leagues", "public");
                });

            modelBuilder.Entity("Exam.Domain.Entities.TeamEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LeagueId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NameCode")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SystemId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LeagueId");

                    b.ToTable("Teams", "public");
                });

            modelBuilder.Entity("Exam.Domain.Entities.UserEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("SystemId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Users", "public");
                });

            modelBuilder.Entity("Exam.Domain.Entities.LeagueEntity", b =>
                {
                    b.HasOne("Exam.Domain.Entities.CategoryEntity", "Category")
                        .WithMany("Leagues")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Exam.Domain.Entities.TeamEntity", b =>
                {
                    b.HasOne("Exam.Domain.Entities.LeagueEntity", "League")
                        .WithMany("Teams")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("League");
                });

            modelBuilder.Entity("Exam.Domain.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Leagues");
                });

            modelBuilder.Entity("Exam.Domain.Entities.LeagueEntity", b =>
                {
                    b.Navigation("Teams");
                });
#pragma warning restore 612, 618
        }
    }
}
