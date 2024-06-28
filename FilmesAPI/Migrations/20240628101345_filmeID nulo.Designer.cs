﻿// <auto-generated />
using System;
using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FilmesAPI.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240628101345_filmeID nulo")]
    partial class filmeIDnulo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FilmesAPI.Models.Cinema", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("enderecoID")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("id");

                    b.HasIndex("enderecoID")
                        .IsUnique();

                    b.ToTable("Cinemas");
                });

            modelBuilder.Entity("FilmesAPI.Models.Endereco", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("logradouro")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("numero")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("FilmesAPI.Models.Filme", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("duracao")
                        .HasColumnType("int");

                    b.Property<string>("genero")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("titulo")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("id");

                    b.ToTable("Filmes");
                });

            modelBuilder.Entity("FilmesAPI.Models.Sessao", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("cinemaID")
                        .HasColumnType("int");

                    b.Property<int?>("filmeID")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("cinemaID");

                    b.HasIndex("filmeID");

                    b.ToTable("Sessoes");
                });

            modelBuilder.Entity("FilmesAPI.Models.Cinema", b =>
                {
                    b.HasOne("FilmesAPI.Models.Endereco", "endereco")
                        .WithOne("Cinema")
                        .HasForeignKey("FilmesAPI.Models.Cinema", "enderecoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("endereco");
                });

            modelBuilder.Entity("FilmesAPI.Models.Sessao", b =>
                {
                    b.HasOne("FilmesAPI.Models.Cinema", "cinema")
                        .WithMany("sessoes")
                        .HasForeignKey("cinemaID");

                    b.HasOne("FilmesAPI.Models.Filme", "filme")
                        .WithMany("sessoes")
                        .HasForeignKey("filmeID");

                    b.Navigation("cinema");

                    b.Navigation("filme");
                });

            modelBuilder.Entity("FilmesAPI.Models.Cinema", b =>
                {
                    b.Navigation("sessoes");
                });

            modelBuilder.Entity("FilmesAPI.Models.Endereco", b =>
                {
                    b.Navigation("Cinema")
                        .IsRequired();
                });

            modelBuilder.Entity("FilmesAPI.Models.Filme", b =>
                {
                    b.Navigation("sessoes");
                });
#pragma warning restore 612, 618
        }
    }
}