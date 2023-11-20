﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WeatherArchive.Data;

#nullable disable

namespace WeatherArchive.Migrations
{
    [DbContext(typeof(WeatherArchiveContext))]
    partial class WeatherArchiveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WeatherArchive.Models.WeatherPhenomenon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("Id");

                    b.ToTable("WeatherPhenomenon", (string)null);
                });

            modelBuilder.Entity("WeatherArchive.Models.WeatherReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<float?>("CloudBase")
                        .HasColumnType("real");

                    b.Property<float?>("Cloudiness")
                        .HasColumnType("real");

                    b.Property<float>("DewPoint")
                        .HasColumnType("real");

                    b.Property<float?>("HorizontalVisibility")
                        .HasColumnType("real");

                    b.Property<float>("Humidity")
                        .HasColumnType("real");

                    b.Property<float>("Pressure")
                        .HasColumnType("real");

                    b.Property<float>("Temperature")
                        .HasColumnType("real");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp");

                    b.Property<float>("WindSpeed")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("Timestamp")
                        .IsUnique();

                    b.ToTable("WeatherReport", (string)null);
                });

            modelBuilder.Entity("WeatherArchive.Models.WindDirection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("WindDirection", (string)null);
                });

            modelBuilder.Entity("WeatherPhenomenonWeatherReport", b =>
                {
                    b.Property<int>("WeatherPhenomenon")
                        .HasColumnType("integer");

                    b.Property<int>("WeatherReport")
                        .HasColumnType("integer");

                    b.HasKey("WeatherPhenomenon", "WeatherReport");

                    b.HasIndex("WeatherReport");

                    b.ToTable("WeatherPhenomenonWeatherReport");
                });

            modelBuilder.Entity("WeatherReportWindDirection", b =>
                {
                    b.Property<int>("WeatherReport")
                        .HasColumnType("integer");

                    b.Property<int>("WindDirection")
                        .HasColumnType("integer");

                    b.HasKey("WeatherReport", "WindDirection");

                    b.HasIndex("WindDirection");

                    b.ToTable("WeatherReportWindDirection");
                });

            modelBuilder.Entity("WeatherPhenomenonWeatherReport", b =>
                {
                    b.HasOne("WeatherArchive.Models.WeatherPhenomenon", null)
                        .WithMany()
                        .HasForeignKey("WeatherPhenomenon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeatherArchive.Models.WeatherReport", null)
                        .WithMany()
                        .HasForeignKey("WeatherReport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WeatherReportWindDirection", b =>
                {
                    b.HasOne("WeatherArchive.Models.WeatherReport", null)
                        .WithMany()
                        .HasForeignKey("WeatherReport")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WeatherArchive.Models.WindDirection", null)
                        .WithMany()
                        .HasForeignKey("WindDirection")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
