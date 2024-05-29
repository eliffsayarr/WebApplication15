﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication15.Models;

#nullable disable

namespace WebApplication15.Migrations
{
    [DbContext(typeof(db_urunTakipContext))]
    [Migration("20240512154205_dadasd")]
    partial class dadasd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplication15.Models.TblKategoriler", b =>
                {
                    b.Property<int>("KategoriId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("KategoriId"), 1L, 1);

                    b.Property<string>("KategoriAd")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("KategoriId");

                    b.ToTable("Tbl_Kategoriler");
                });

            modelBuilder.Entity("WebApplication15.Models.TblUrun", b =>
                {
                    b.Property<int>("UrunId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UrunId"), 1L, 1);

                    b.Property<int?>("KategoriId")
                        .HasColumnType("int");

                    b.Property<string>("UrunAd")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<short?>("UrunAdet")
                        .HasColumnType("smallint");

                    b.Property<decimal?>("UrunFiyat")
                        .HasColumnType("money");

                    b.Property<string>("UrunPhoto")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.HasKey("UrunId");

                    b.HasIndex("KategoriId");

                    b.ToTable("Tbl_Urun");
                });

            modelBuilder.Entity("WebApplication15.Models.TblUrun", b =>
                {
                    b.HasOne("WebApplication15.Models.TblKategoriler", "TblKategoriler")
                        .WithMany("TblUruns")
                        .HasForeignKey("KategoriId");

                    b.Navigation("TblKategoriler");
                });

            modelBuilder.Entity("WebApplication15.Models.TblKategoriler", b =>
                {
                    b.Navigation("TblUruns");
                });
#pragma warning restore 612, 618
        }
    }
}
