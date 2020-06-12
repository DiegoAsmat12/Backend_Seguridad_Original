﻿// <auto-generated />
using System;
using Backend_Seguridad.Contexts.Software;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Backend_Seguridad.Migrations.ApplicationSoftwareDb
{
    [DbContext(typeof(ApplicationSoftwareDbContext))]
    [Migration("20200331165236_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Backend_Seguridad.Entities.Software.Camara", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Latitud")
                        .HasColumnType("double");

                    b.Property<double>("Longitud")
                        .HasColumnType("double");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Camaras");
                });

            modelBuilder.Entity("Backend_Seguridad.Entities.Software.Placa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("CamaraDetectoraId")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Direccion")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Fecha")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Hora")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte[]>("ImagenCarro")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("ImagenPlaca")
                        .HasColumnType("longblob");

                    b.Property<double>("Latitud")
                        .HasColumnType("double");

                    b.Property<double>("Longitud")
                        .HasColumnType("double");

                    b.Property<string>("NombreCamara")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NumeroDePlaca")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CamaraDetectoraId");

                    b.ToTable("Placas");
                });

            modelBuilder.Entity("Backend_Seguridad.Entities.Software.Placa", b =>
                {
                    b.HasOne("Backend_Seguridad.Entities.Software.Camara", "CamaraDetectora")
                        .WithMany("Placas")
                        .HasForeignKey("CamaraDetectoraId");
                });
#pragma warning restore 612, 618
        }
    }
}
