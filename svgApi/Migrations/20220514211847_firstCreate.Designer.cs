// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using svgApi.Controllers;

#nullable disable

namespace svgApi.Migrations
{
    [DbContext(typeof(RectangleContext))]
    [Migration("20220514211847_firstCreate")]
    partial class firstCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("svgApi.Models.Rectangle", b =>
                {
                    b.Property<int>("RectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RectId"), 1L, 1);

                    b.Property<int?>("Height")
                        .HasColumnType("int");

                    b.Property<int>("SvgId")
                        .HasColumnType("int");

                    b.Property<int?>("Width")
                        .HasColumnType("int");

                    b.Property<int?>("XCoordinate")
                        .HasColumnType("int");

                    b.Property<int?>("YCoordinate")
                        .HasColumnType("int");

                    b.HasKey("RectId");

                    b.ToTable("Rectangles");
                });
#pragma warning restore 612, 618
        }
    }
}
