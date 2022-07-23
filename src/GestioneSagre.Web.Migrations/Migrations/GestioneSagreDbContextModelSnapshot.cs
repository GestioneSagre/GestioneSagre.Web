﻿// <auto-generated />
using GestioneSagre.Business.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestioneSagre.Web.Migrations.Migrations
{
    [DbContext(typeof(GestioneSagreDbContext))]
    partial class GestioneSagreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.CategoriaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CategoriaStampa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CategoriaVideo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuidFesta")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categoria", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.FestaEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DataFine")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DataInizio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GuidFesta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusFesta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Festa", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.IntestazioneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Edizione")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FestaId")
                        .HasColumnType("int");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Luogo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titolo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FestaId");

                    b.ToTable("Intestazione", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.ProdottoEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AvvisoScorta")
                        .HasColumnType("bit");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("GuidFesta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Prenotazione")
                        .HasColumnType("bit");

                    b.Property<string>("Prodotto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ProdottoAttivo")
                        .HasColumnType("bit");

                    b.Property<int>("Quantita")
                        .HasColumnType("int");

                    b.Property<bool>("QuantitaAttiva")
                        .HasColumnType("bit");

                    b.Property<int>("QuantitaScorta")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Prodotto", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.VersioneEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CodiceVersione")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TestoVersione")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VersioneStato")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Versione", (string)null);
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.IntestazioneEntity", b =>
                {
                    b.HasOne("GestioneSagre.Infrastructure.Entities.FestaEntity", "Festa")
                        .WithMany("Intestazioni")
                        .HasForeignKey("FestaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Festa");
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.ProdottoEntity", b =>
                {
                    b.HasOne("GestioneSagre.Infrastructure.Entities.CategoriaEntity", "Categoria")
                        .WithMany("Prodotti")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("GestioneSagre.Infrastructure.ValueObjects.Money", "Prezzo", b1 =>
                        {
                            b1.Property<int>("ProdottoEntityId")
                                .HasColumnType("int");

                            b1.Property<float>("Amount")
                                .HasColumnType("real");

                            b1.Property<string>("Currency")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProdottoEntityId");

                            b1.ToTable("Prodotto");

                            b1.WithOwner()
                                .HasForeignKey("ProdottoEntityId");
                        });

                    b.Navigation("Categoria");

                    b.Navigation("Prezzo");
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.CategoriaEntity", b =>
                {
                    b.Navigation("Prodotti");
                });

            modelBuilder.Entity("GestioneSagre.Infrastructure.Entities.FestaEntity", b =>
                {
                    b.Navigation("Intestazioni");
                });
#pragma warning restore 612, 618
        }
    }
}
