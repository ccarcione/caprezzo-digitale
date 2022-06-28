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
    [Migration("20210714155648_fix-nome-tabbela-feedback")]
    partial class fixnometabbelafeedback
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.Allegato", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descrizione")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<long>("MessaggioId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("MessaggioId");

                    b.ToTable("Allegati");
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.BollettinoArpa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("PDF_FileName")
                        .HasColumnType("text");

                    b.Property<string>("Tipo")
                        .HasColumnType("text");

                    b.Property<string>("XML_FileName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BollettiniArpa");
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.EmailFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Messaggio")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<short>("Rating")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.Messaggio", b =>
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

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.Statistica", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("timestamp without time zone");

                    b.Property<short>("TipoStatisticaId")
                        .HasColumnType("smallint");

                    b.Property<string>("Valore")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("TipoStatisticaId");

                    b.ToTable("Statistiche");
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.TipoMessaggio", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Colore")
                        .HasColumnType("text");

                    b.Property<string>("Descrizione")
                        .HasColumnType("text");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Icona")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TipiMessaggio");
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.TipoStatistica", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Descrizione")
                        .HasColumnType("text");

                    b.Property<string>("Tipo")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TipiStatistica");
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.Allegato", b =>
                {
                    b.HasOne("CaprezzoDigitale.WebApi.Models.Messaggio", "Messaggio")
                        .WithMany("Allegati")
                        .HasForeignKey("MessaggioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.Messaggio", b =>
                {
                    b.HasOne("CaprezzoDigitale.WebApi.Models.TipoMessaggio", "TipoMessaggio")
                        .WithMany("Messaggi")
                        .HasForeignKey("TipoMessaggioId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("CaprezzoDigitale.WebApi.Models.Statistica", b =>
                {
                    b.HasOne("CaprezzoDigitale.WebApi.Models.TipoStatistica", "TipoStatistica")
                        .WithMany("Statistiche")
                        .HasForeignKey("TipoStatisticaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
