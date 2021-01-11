﻿// <auto-generated />
using System;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLayer.Migrations
{
    [DbContext(typeof(ProjekatContext))]
    [Migration("20210107164031_D3")]
    partial class D3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DataLayer.Models.Kategorija", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Kategorije");
                });

            modelBuilder.Entity("DataLayer.Models.Korisnik", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Korisnici");
                });

            modelBuilder.Entity("DataLayer.Models.Potez", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("BojaLinije")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Crtanje")
                        .HasColumnType("bit");

                    b.Property<int?>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<string>("ParametarLinije")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Poruka")
                        .HasColumnType("bit");

                    b.Property<string>("TekstPoruke")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TokIgreId")
                        .HasColumnType("int");

                    b.Property<DateTime>("VremePoteza")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("TokIgreId");

                    b.ToTable("Potezi");
                });

            modelBuilder.Entity("DataLayer.Models.Rec", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Reci");
                });

            modelBuilder.Entity("DataLayer.Models.RecPoKategoriji", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("KategorijaId")
                        .HasColumnType("int");

                    b.Property<int>("RecId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KategorijaId");

                    b.HasIndex("RecId");

                    b.ToTable("ReciPoKategorijama");
                });

            modelBuilder.Entity("DataLayer.Models.TokIgre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("PocetakIgre")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RecZaPogadjanjeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RecZaPogadjanjeId");

                    b.ToTable("TokoviIgre");
                });

            modelBuilder.Entity("DataLayer.Models.TokIgrePoKorisniku", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("KorisnikId")
                        .HasColumnType("int");

                    b.Property<int?>("TokIgreId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("KorisnikId");

                    b.HasIndex("TokIgreId");

                    b.ToTable("TokoviIgrePoKorisniku");
                });

            modelBuilder.Entity("DataLayer.Models.Potez", b =>
                {
                    b.HasOne("DataLayer.Models.Korisnik", "Korisnik")
                        .WithMany("Potezi")
                        .HasForeignKey("KorisnikId");

                    b.HasOne("DataLayer.Models.TokIgre", "TokIgre")
                        .WithMany("Potezi")
                        .HasForeignKey("TokIgreId");

                    b.Navigation("Korisnik");

                    b.Navigation("TokIgre");
                });

            modelBuilder.Entity("DataLayer.Models.RecPoKategoriji", b =>
                {
                    b.HasOne("DataLayer.Models.Kategorija", "Kategorija")
                        .WithMany("ReciPoKategorijama")
                        .HasForeignKey("KategorijaId");

                    b.HasOne("DataLayer.Models.Rec", "Rec")
                        .WithMany("RecPoKategoriji")
                        .HasForeignKey("RecId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kategorija");

                    b.Navigation("Rec");
                });

            modelBuilder.Entity("DataLayer.Models.TokIgre", b =>
                {
                    b.HasOne("DataLayer.Models.Rec", "RecZaPogadjanje")
                        .WithMany()
                        .HasForeignKey("RecZaPogadjanjeId");

                    b.Navigation("RecZaPogadjanje");
                });

            modelBuilder.Entity("DataLayer.Models.TokIgrePoKorisniku", b =>
                {
                    b.HasOne("DataLayer.Models.Korisnik", "Korisnik")
                        .WithMany("TokIgrePoKorisniku")
                        .HasForeignKey("KorisnikId");

                    b.HasOne("DataLayer.Models.TokIgre", "TokIgre")
                        .WithMany("TokIgrePoKorisniku")
                        .HasForeignKey("TokIgreId");

                    b.Navigation("Korisnik");

                    b.Navigation("TokIgre");
                });

            modelBuilder.Entity("DataLayer.Models.Kategorija", b =>
                {
                    b.Navigation("ReciPoKategorijama");
                });

            modelBuilder.Entity("DataLayer.Models.Korisnik", b =>
                {
                    b.Navigation("Potezi");

                    b.Navigation("TokIgrePoKorisniku");
                });

            modelBuilder.Entity("DataLayer.Models.Rec", b =>
                {
                    b.Navigation("RecPoKategoriji");
                });

            modelBuilder.Entity("DataLayer.Models.TokIgre", b =>
                {
                    b.Navigation("Potezi");

                    b.Navigation("TokIgrePoKorisniku");
                });
#pragma warning restore 612, 618
        }
    }
}
