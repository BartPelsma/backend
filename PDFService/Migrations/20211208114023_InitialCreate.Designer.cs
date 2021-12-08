﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PDFService.DBContexts;

namespace PDFService.Migrations
{
    [DbContext(typeof(PDFServiceDatabaseContext))]
    [Migration("20211208114023_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PDFService.Models.Pdf", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("Blob")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("LinkedKey")
                        .HasColumnType("int");

                    b.Property<int>("LinkedTableType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("pdfs");
                });
#pragma warning restore 612, 618
        }
    }
}
