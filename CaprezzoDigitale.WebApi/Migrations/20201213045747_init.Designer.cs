﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using CaprezzoDigitale.WebApi.Models;

namespace CaprezzoDigitale.WebApi.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20201213045747_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("WebApi.caprezzo_digitale.Models.Allegato", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descrizione")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<long>("MessaggioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MessaggioId");

                    b.ToTable("Allegati");
                });

            modelBuilder.Entity("WebApi.caprezzo_digitale.Models.Messaggio", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DataPubblicazione")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Sottotitolo")
                        .HasColumnType("text");

                    b.Property<string>("Testo")
                        .HasColumnType("text");

                    b.Property<short>("TipoMessaggioId")
                        .HasColumnType("smallint");

                    b.Property<string>("Titolo")
                        .HasColumnType("text");

                    b.Property<string>("UrlImmagine")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TipoMessaggioId");

                    b.ToTable("Messaggi");
                });

            modelBuilder.Entity("WebApi.caprezzo_digitale.Models.TipoMessaggio", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descrizione")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TipiMessaggio");
                });

            modelBuilder.Entity("WebApi.caprezzo_digitale.Models.Allegato", b =>
                {
                    b.HasOne("WebApi.caprezzo_digitale.Models.Messaggio", null)
                        .WithMany("Allegati")
                        .HasForeignKey("MessaggioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApi.caprezzo_digitale.Models.Messaggio", b =>
                {
                    b.HasOne("WebApi.caprezzo_digitale.Models.TipoMessaggio", "TipoMessaggio")
                        .WithMany()
                        .HasForeignKey("TipoMessaggioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
