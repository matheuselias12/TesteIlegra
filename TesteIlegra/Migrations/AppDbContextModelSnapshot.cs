﻿// <auto-generated />
using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TesteIlegra.Data;

#nullable disable

namespace TesteIlegra.Migrations
{
    [ExcludeFromCodeCoverage]
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TesteIlegra.Domain.Modelo.PedidoCliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cliente")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PedidosClientes");
                });

            modelBuilder.Entity("TesteIlegra.Domain.Modelo.Revenda", b =>
                {
                    b.Property<Guid>("RevendaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomeFantasia")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazaoSocial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RevendaId");

                    b.ToTable("Revendas");
                });

            modelBuilder.Entity("TesteIlegra.Domain.Modelo.PedidoCliente", b =>
                {
                    b.OwnsMany("TesteIlegra.Domain.Modelo.ItemPedido", "Itens", b1 =>
                        {
                            b1.Property<Guid>("PedidoClienteId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Produto")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Quantidade")
                                .HasColumnType("int");

                            b1.HasKey("PedidoClienteId", "Id");

                            b1.ToTable("ItensPedidoCliente", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("PedidoClienteId");
                        });

                    b.Navigation("Itens");
                });

            modelBuilder.Entity("TesteIlegra.Domain.Modelo.Revenda", b =>
                {
                    b.OwnsMany("TesteIlegra.Domain.Modelo.Contato", "Contatos", b1 =>
                        {
                            b1.Property<Guid>("RevendaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Nome")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<bool>("Principal")
                                .HasColumnType("bit");

                            b1.HasKey("RevendaId", "Id");

                            b1.ToTable("Contatos", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RevendaId");
                        });

                    b.OwnsMany("TesteIlegra.Domain.Modelo.Endereco", "EnderecosEntrega", b1 =>
                        {
                            b1.Property<Guid>("RevendaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Cep")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Estado")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Rua")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("RevendaId", "Id");

                            b1.ToTable("EnderecosEntrega", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RevendaId");
                        });

                    b.OwnsMany("TesteIlegra.Domain.Modelo.Telefone", "Telefones", b1 =>
                        {
                            b1.Property<Guid>("RevendaId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("RevendaId", "Id");

                            b1.ToTable("Telefones", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RevendaId");
                        });

                    b.Navigation("Contatos");

                    b.Navigation("EnderecosEntrega");

                    b.Navigation("Telefones");
                });
#pragma warning restore 612, 618
        }
    }
}
