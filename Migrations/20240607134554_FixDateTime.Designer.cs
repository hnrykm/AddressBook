﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Backend.Interview.Api.Migrations
{
    [DbContext(typeof(BackendInterviewDbContext))]
    [Migration("20240607134554_FixDateTime")]
    partial class FixDateTime
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Backend.Interview.Api.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("Backend.Interview.Api.Models.Person", b =>
                {
                    b.OwnsOne("Backend.Interview.Api.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uuid");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Line1")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("Line2")
                                .HasColumnType("text");

                            b1.Property<string>("State")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ZipCode")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("PersonId");

                            b1.ToTable("People");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
