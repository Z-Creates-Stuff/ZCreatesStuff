﻿// <auto-generated />
using System;
using LouiseTieDyeStore.Server.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LouiseTieDyeStore.Server.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240403061913_OrderItems_FK")]
    partial class OrderItems_FK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("LineOne")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LineTwo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Zip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.CartItem", b =>
                {
                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserEmail", "ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Women",
                            Url = "women"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Men",
                            Url = "men"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Kids",
                            Url = "kids"
                        });
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ProductId = 1,
                            Url = "https://i.imgur.com/1DnQj7V.jpg"
                        },
                        new
                        {
                            Id = 2,
                            ProductId = 1,
                            Url = "https://i.imgur.com/tGJkEL9.jpg"
                        },
                        new
                        {
                            Id = 3,
                            ProductId = 2,
                            Url = "https://i.imgur.com/zw3Olrp.jpg"
                        },
                        new
                        {
                            Id = 4,
                            ProductId = 2,
                            Url = "https://i.imgur.com/9uEVXR1.png"
                        },
                        new
                        {
                            Id = 5,
                            ProductId = 3,
                            Url = "https://i.imgur.com/uHKlJtV.jpg"
                        },
                        new
                        {
                            Id = 6,
                            ProductId = 3,
                            Url = "https://i.imgur.com/MD7Wzx4.jpg"
                        });
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SalesTax")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("ShippingCost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("SubTotal")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.OrderItem", b =>
                {
                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("OriginalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Sold")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            Deleted = false,
                            Description = "Blue and Yellow pattern on a womens ScoobyDoo t-shirt",
                            OriginalPrice = 35.00m,
                            Price = 30.00m,
                            ProductTypeId = 1,
                            Size = "XL",
                            Sold = false,
                            Title = "Cool TyeDye Shirt",
                            Visible = true
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            Deleted = false,
                            Description = "Blue and Yellow pattern on a Long Sleeve Shirt",
                            OriginalPrice = 25.00m,
                            Price = 20.00m,
                            ProductTypeId = 2,
                            Size = "S",
                            Sold = false,
                            Title = "Really Cool TyeDye Shirt",
                            Visible = true
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            Deleted = false,
                            Description = "Blue and Red spiral pattern on a baby onesie.",
                            OriginalPrice = 15.00m,
                            Price = 10.00m,
                            ProductTypeId = 3,
                            Size = "M",
                            Sold = false,
                            Title = "TyeDye Onesie",
                            Visible = true
                        });
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "T-Shirt"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Long Sleeve Shirt"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Onesie"
                        });
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.TaxRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Rate")
                        .HasColumnType("decimal(18,3)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TaxRates");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Abbreviation = "AL",
                            Rate = 4.000m,
                            State = "Alabama"
                        },
                        new
                        {
                            Id = 2,
                            Abbreviation = "AK",
                            Rate = 0.000m,
                            State = "Alaska"
                        },
                        new
                        {
                            Id = 3,
                            Abbreviation = "AZ",
                            Rate = 5.600m,
                            State = "Arizona"
                        },
                        new
                        {
                            Id = 4,
                            Abbreviation = "AR",
                            Rate = 6.500m,
                            State = "Arkansas"
                        },
                        new
                        {
                            Id = 5,
                            Abbreviation = "CA",
                            Rate = 7.250m,
                            State = "California"
                        },
                        new
                        {
                            Id = 6,
                            Abbreviation = "CO",
                            Rate = 2.900m,
                            State = "Colorado"
                        },
                        new
                        {
                            Id = 7,
                            Abbreviation = "CT",
                            Rate = 6.350m,
                            State = "Connecticut"
                        },
                        new
                        {
                            Id = 8,
                            Abbreviation = "DE",
                            Rate = 0.000m,
                            State = "Delaware"
                        },
                        new
                        {
                            Id = 9,
                            Abbreviation = "FL",
                            Rate = 6.000m,
                            State = "Florida"
                        },
                        new
                        {
                            Id = 10,
                            Abbreviation = "GA",
                            Rate = 4.000m,
                            State = "Georgia"
                        },
                        new
                        {
                            Id = 11,
                            Abbreviation = "HI",
                            Rate = 4.000m,
                            State = "Hawaii"
                        },
                        new
                        {
                            Id = 12,
                            Abbreviation = "ID",
                            Rate = 6.000m,
                            State = "Idaho"
                        },
                        new
                        {
                            Id = 13,
                            Abbreviation = "IL",
                            Rate = 6.250m,
                            State = "Illinois"
                        },
                        new
                        {
                            Id = 14,
                            Abbreviation = "IN",
                            Rate = 7.000m,
                            State = "Indiana"
                        },
                        new
                        {
                            Id = 15,
                            Abbreviation = "IA",
                            Rate = 6.000m,
                            State = "Iowa"
                        },
                        new
                        {
                            Id = 16,
                            Abbreviation = "KS",
                            Rate = 6.500m,
                            State = "Kansas"
                        },
                        new
                        {
                            Id = 17,
                            Abbreviation = "KY",
                            Rate = 6.000m,
                            State = "Kentucky"
                        },
                        new
                        {
                            Id = 18,
                            Abbreviation = "LA",
                            Rate = 4.450m,
                            State = "Louisiana"
                        },
                        new
                        {
                            Id = 19,
                            Abbreviation = "ME",
                            Rate = 5.500m,
                            State = "Maine"
                        },
                        new
                        {
                            Id = 20,
                            Abbreviation = "MD",
                            Rate = 6.000m,
                            State = "Maryland"
                        },
                        new
                        {
                            Id = 21,
                            Abbreviation = "MA",
                            Rate = 6.250m,
                            State = "Massachusetts"
                        },
                        new
                        {
                            Id = 22,
                            Abbreviation = "MI",
                            Rate = 6.000m,
                            State = "Michigan"
                        },
                        new
                        {
                            Id = 23,
                            Abbreviation = "MN",
                            Rate = 6.875m,
                            State = "Minnesota"
                        },
                        new
                        {
                            Id = 24,
                            Abbreviation = "MS",
                            Rate = 7.000m,
                            State = "Mississippi"
                        },
                        new
                        {
                            Id = 25,
                            Abbreviation = "MO",
                            Rate = 4.225m,
                            State = "Missouri"
                        },
                        new
                        {
                            Id = 26,
                            Abbreviation = "MT",
                            Rate = 0.000m,
                            State = "Montana"
                        },
                        new
                        {
                            Id = 27,
                            Abbreviation = "NE",
                            Rate = 5.500m,
                            State = "Nebraska"
                        },
                        new
                        {
                            Id = 28,
                            Abbreviation = "NV",
                            Rate = 6.850m,
                            State = "Nevada"
                        },
                        new
                        {
                            Id = 29,
                            Abbreviation = "NH",
                            Rate = 0.000m,
                            State = "New Hampshire"
                        },
                        new
                        {
                            Id = 30,
                            Abbreviation = "NJ",
                            Rate = 6.625m,
                            State = "New Jersey"
                        },
                        new
                        {
                            Id = 31,
                            Abbreviation = "MN",
                            Rate = 4.875m,
                            State = "New Mexico"
                        },
                        new
                        {
                            Id = 32,
                            Abbreviation = "NY",
                            Rate = 4.000m,
                            State = "New York"
                        },
                        new
                        {
                            Id = 33,
                            Abbreviation = "NC",
                            Rate = 4.750m,
                            State = "North Carolina"
                        },
                        new
                        {
                            Id = 34,
                            Abbreviation = "ND",
                            Rate = 5.000m,
                            State = "North Dakota"
                        },
                        new
                        {
                            Id = 35,
                            Abbreviation = "OH",
                            Rate = 5.750m,
                            State = "Ohio"
                        },
                        new
                        {
                            Id = 36,
                            Abbreviation = "OK",
                            Rate = 4.500m,
                            State = "Oklahoma"
                        },
                        new
                        {
                            Id = 37,
                            Abbreviation = "OR",
                            Rate = 0.000m,
                            State = "Oregon"
                        },
                        new
                        {
                            Id = 38,
                            Abbreviation = "PA",
                            Rate = 6.000m,
                            State = "Pennsylvania"
                        },
                        new
                        {
                            Id = 39,
                            Abbreviation = "RI",
                            Rate = 7.000m,
                            State = "Rhode Island"
                        },
                        new
                        {
                            Id = 40,
                            Abbreviation = "SC",
                            Rate = 6.000m,
                            State = "South Carolina"
                        },
                        new
                        {
                            Id = 41,
                            Abbreviation = "SD",
                            Rate = 4.200m,
                            State = "South Dakota"
                        },
                        new
                        {
                            Id = 42,
                            Abbreviation = "TN",
                            Rate = 7.000m,
                            State = "Tennessee"
                        },
                        new
                        {
                            Id = 43,
                            Abbreviation = "TX",
                            Rate = 6.250m,
                            State = "Texas"
                        },
                        new
                        {
                            Id = 44,
                            Abbreviation = "UT",
                            Rate = 6.100m,
                            State = "Utah"
                        },
                        new
                        {
                            Id = 45,
                            Abbreviation = "VT",
                            Rate = 6.000m,
                            State = "Vermont"
                        },
                        new
                        {
                            Id = 46,
                            Abbreviation = "VA",
                            Rate = 5.300m,
                            State = "Virginia"
                        },
                        new
                        {
                            Id = 47,
                            Abbreviation = "WA",
                            Rate = 6.500m,
                            State = "Washington"
                        },
                        new
                        {
                            Id = 48,
                            Abbreviation = "WV",
                            Rate = 6.000m,
                            State = "West Virginia"
                        },
                        new
                        {
                            Id = 49,
                            Abbreviation = "WI",
                            Rate = 5.000m,
                            State = "Wisconsin"
                        },
                        new
                        {
                            Id = 50,
                            Abbreviation = "WY",
                            Rate = 4.000m,
                            State = "Wyoming"
                        },
                        new
                        {
                            Id = 51,
                            Abbreviation = "DC",
                            Rate = 6.000m,
                            State = "District of Columbia"
                        });
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Address", b =>
                {
                    b.HasOne("LouiseTieDyeStore.Shared.Order", null)
                        .WithOne("Address")
                        .HasForeignKey("LouiseTieDyeStore.Shared.Address", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Image", b =>
                {
                    b.HasOne("LouiseTieDyeStore.Shared.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.OrderItem", b =>
                {
                    b.HasOne("LouiseTieDyeStore.Shared.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LouiseTieDyeStore.Shared.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Product", b =>
                {
                    b.HasOne("LouiseTieDyeStore.Shared.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LouiseTieDyeStore.Shared.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Order", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("LouiseTieDyeStore.Shared.Product", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
