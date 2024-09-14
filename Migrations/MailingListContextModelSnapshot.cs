﻿// <auto-generated />
using System;
using MailingListDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HW_7._1.Migrations
{
    [DbContext(typeof(MailingListContext))]
    partial class MailingListContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomerSection", b =>
                {
                    b.Property<int>("CustomersCustomerId")
                        .HasColumnType("int");

                    b.Property<int>("InterestedSectionsSectionId")
                        .HasColumnType("int");

                    b.HasKey("CustomersCustomerId", "InterestedSectionsSectionId");

                    b.HasIndex("InterestedSectionsSectionId");

                    b.ToTable("CustomerSection");
                });

            modelBuilder.Entity("MailingListDB.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CityId"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("MailingListDB.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("MailingListDB.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("MailingListDB.Promotion", b =>
                {
                    b.Property<int>("PromotionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PromotionId"));

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SectionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("PromotionId");

                    b.HasIndex("CountryId");

                    b.HasIndex("SectionId");

                    b.ToTable("Promotions");
                });

            modelBuilder.Entity("MailingListDB.Section", b =>
                {
                    b.Property<int>("SectionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SectionId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectionId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("CustomerSection", b =>
                {
                    b.HasOne("MailingListDB.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomersCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MailingListDB.Section", null)
                        .WithMany()
                        .HasForeignKey("InterestedSectionsSectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MailingListDB.City", b =>
                {
                    b.HasOne("MailingListDB.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MailingListDB.Customer", b =>
                {
                    b.HasOne("MailingListDB.City", "City")
                        .WithMany("Customers")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MailingListDB.Country", "Country")
                        .WithMany("Customers")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("City");

                    b.Navigation("Country");
                });

            modelBuilder.Entity("MailingListDB.Promotion", b =>
                {
                    b.HasOne("MailingListDB.Country", "Country")
                        .WithMany("Promotions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MailingListDB.Section", "Section")
                        .WithMany("Promotions")
                        .HasForeignKey("SectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Section");
                });

            modelBuilder.Entity("MailingListDB.City", b =>
                {
                    b.Navigation("Customers");
                });

            modelBuilder.Entity("MailingListDB.Country", b =>
                {
                    b.Navigation("Cities");

                    b.Navigation("Customers");

                    b.Navigation("Promotions");
                });

            modelBuilder.Entity("MailingListDB.Section", b =>
                {
                    b.Navigation("Promotions");
                });
#pragma warning restore 612, 618
        }
    }
}
