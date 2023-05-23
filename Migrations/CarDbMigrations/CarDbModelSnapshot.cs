﻿// <auto-generated />
using LinqQueries;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LinqQueries.Migrations.CarDbMigrations
{
    [DbContext(typeof(CarDb))]
    partial class CarDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.5");

            modelBuilder.Entity("LinqQueries.CarModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("City")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Combined")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cylinders")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Displacement")
                        .HasColumnType("REAL");

                    b.Property<int>("Highway")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Cars");
                });
#pragma warning restore 612, 618
        }
    }
}
