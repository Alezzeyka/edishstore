// <auto-generated />
using System;
using ASPLab.Data.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASPLab.Migrations
{
    [DbContext(typeof(AppDBContent))]
    [Migration("20220626183359_UserCascadeDelete")]
    partial class UserCascadeDelete
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.16")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.HasSequence<int>("OrderNumbers");

            modelBuilder.Entity("ASPLab.Data.Models.Category", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ASPLab.Data.Models.Dish", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CategoryID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Color")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Material")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("Volume")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("ASPLab.Data.Models.Order", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("OrderNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValueSql("NEXT VALUE FOR OrderNumbers");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("orderTime")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ASPLab.Data.Models.OrderDetail", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DishID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrderID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("DishID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("ASPLab.Data.Models.ShopCartItem", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DishID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<Guid>("ShopCartID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ID");

                    b.HasIndex("DishID");

                    b.ToTable("ShopCartItems");
                });

            modelBuilder.Entity("ASPLab.Data.Models.User", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(16)
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ASPLab.Data.Models.Dish", b =>
                {
                    b.HasOne("ASPLab.Data.Models.Category", "Category")
                        .WithMany("Dishes")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ASPLab.Data.Models.Order", b =>
                {
                    b.HasOne("ASPLab.Data.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("User");
                });

            modelBuilder.Entity("ASPLab.Data.Models.OrderDetail", b =>
                {
                    b.HasOne("ASPLab.Data.Models.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishID");

                    b.HasOne("ASPLab.Data.Models.Order", "Order")
                        .WithMany("OrgerDetails")
                        .HasForeignKey("OrderID");

                    b.Navigation("Dish");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("ASPLab.Data.Models.ShopCartItem", b =>
                {
                    b.HasOne("ASPLab.Data.Models.Dish", "Dish")
                        .WithMany()
                        .HasForeignKey("DishID");

                    b.Navigation("Dish");
                });

            modelBuilder.Entity("ASPLab.Data.Models.Category", b =>
                {
                    b.Navigation("Dishes");
                });

            modelBuilder.Entity("ASPLab.Data.Models.Order", b =>
                {
                    b.Navigation("OrgerDetails");
                });

            modelBuilder.Entity("ASPLab.Data.Models.User", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
