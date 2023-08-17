﻿// <auto-generated />
using System;
using GiveFreely.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GiveFreely.DataAccess.Migrations
{
    [DbContext(typeof(GFContext))]
    partial class GFContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GiveFreely.DataAccess.Schema.Affiliate", b =>
                {
                    b.Property<int>("IdAffiliate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAffiliate"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAffiliate");

                    b.ToTable("Affiliates", (string)null);
                });

            modelBuilder.Entity("GiveFreely.DataAccess.Schema.Commision", b =>
                {
                    b.Property<int>("IdCommusion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCommusion"), 1L, 1);

                    b.Property<int>("FromCount")
                        .HasColumnType("int");

                    b.Property<decimal>("Money")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ToCount")
                        .HasColumnType("int");

                    b.HasKey("IdCommusion");

                    b.ToTable("Commisions", (string)null);
                });

            modelBuilder.Entity("GiveFreely.DataAccess.Schema.Customer", b =>
                {
                    b.Property<int>("IdCustomer")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCustomer"), 1L, 1);

                    b.Property<int>("IdAffiliate")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCustomer");

                    b.HasIndex("IdAffiliate");

                    b.ToTable("Customers", (string)null);
                });

            modelBuilder.Entity("GiveFreely.DataAccess.Schema.Customer", b =>
                {
                    b.HasOne("GiveFreely.DataAccess.Schema.Affiliate", "Affiliate")
                        .WithMany("Customers")
                        .HasForeignKey("IdAffiliate")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Affiliate");
                });

            modelBuilder.Entity("GiveFreely.DataAccess.Schema.Affiliate", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}