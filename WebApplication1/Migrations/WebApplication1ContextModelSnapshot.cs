﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Data;

#nullable disable

namespace WebApplication1.Migrations
{
    [DbContext(typeof(WebApplication1Context))]
    partial class WebApplication1ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("WebApplication1.Data.CodingRegion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GenomeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Header")
                        .HasMaxLength(1000)
                        .HasColumnType("TEXT");

                    b.Property<int>("Location")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LocusTag")
                        .HasColumnType("TEXT");

                    b.Property<int?>("OrthoGroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Sequence")
                        .HasColumnType("TEXT");

                    b.Property<string>("genomeFile")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GenomeId");

                    b.HasIndex("OrthoGroupId");

                    b.ToTable("CodingRegions");
                });

            modelBuilder.Entity("WebApplication1.Data.Genome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Genomes");
                });

            modelBuilder.Entity("WebApplication1.Data.OrthoGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("OrthoGroup");
                });

            modelBuilder.Entity("WebApplication1.Data.CodingRegion", b =>
                {
                    b.HasOne("WebApplication1.Data.Genome", null)
                        .WithMany("Proteins")
                        .HasForeignKey("GenomeId");

                    b.HasOne("WebApplication1.Data.OrthoGroup", null)
                        .WithMany("Proteins")
                        .HasForeignKey("OrthoGroupId");
                });

            modelBuilder.Entity("WebApplication1.Data.Genome", b =>
                {
                    b.Navigation("Proteins");
                });

            modelBuilder.Entity("WebApplication1.Data.OrthoGroup", b =>
                {
                    b.Navigation("Proteins");
                });
#pragma warning restore 612, 618
        }
    }
}
