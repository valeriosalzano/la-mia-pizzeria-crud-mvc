﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using la_mia_pizzeria.Database;

#nullable disable

namespace la_mia_pizzeria.Migrations
{
    [DbContext(typeof(PizzeriaContext))]
    partial class PizzeriaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("la_mia_pizzeria.Models.Pizza", b =>
                {
                    b.Property<int>("PizzaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PizzaId"));

                    b.Property<string>("Description")
                        .HasColumnType("VARCHAR(1000)")
                        .HasColumnName("description");

                    b.Property<string>("ImgPath")
                        .HasColumnType("VARCHAR(1000)")
                        .HasColumnName("img_path");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("DECIMAL(5, 2)")
                        .HasColumnName("price");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("VARCHAR(100)")
                        .HasColumnName("slug");

                    b.HasKey("PizzaId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("pizzas");
                });
#pragma warning restore 612, 618
        }
    }
}