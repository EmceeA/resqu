﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Resqu.Core.Entities;

namespace Resqu.Core.Migrations
{
    [DbContext(typeof(ResquContext))]
    [Migration("20210411090650_added models")]
    partial class addedmodels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Resqu.Core.Entities.CardPayment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CustomerCardNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Cvv")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorWalletId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CardPayments");
                });

            modelBuilder.Entity("Resqu.Core.Entities.CashPayment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerPhoneNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VendorId")
                        .HasColumnType("int");

                    b.Property<string>("VendorPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VendorId");

                    b.ToTable("CashPayments");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFullyVerified")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RegulatoryIndentity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isVerified")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Expertise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExpertiseCategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsModified")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isVerified")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ExpertiseCategoryId");

                    b.ToTable("Expertises");
                });

            modelBuilder.Entity("Resqu.Core.Entities.ExpertiseCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ExpertiseCategories");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Otp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DateModified")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GetCustomerId")
                        .HasColumnType("int");

                    b.Property<string>("OtpNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GetCustomerId");

                    b.ToTable("Otps");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Request", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CustomerLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAccepted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRejected")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GetExpertiseCategoryId")
                        .HasColumnType("int");

                    b.Property<int?>("GetExpertiseId")
                        .HasColumnType("int");

                    b.Property<string>("VendorLocation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GetExpertiseCategoryId");

                    b.HasIndex("GetExpertiseId");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("Resqu.Core.Entities.ResquProcess", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CustomerRating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GetCustomerId")
                        .HasColumnType("int");

                    b.Property<long?>("GetRequestId")
                        .HasColumnType("bigint");

                    b.Property<int?>("GetVendorId")
                        .HasColumnType("int");

                    b.Property<string>("ProcessAction")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VendorRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GetCustomerId");

                    b.HasIndex("GetRequestId");

                    b.HasIndex("GetVendorId");

                    b.ToTable("ResquProcesses");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ContactAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBan")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFullyVerified")
                        .HasColumnType("bit");

                    b.Property<string>("NextOfKin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextOfKinAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextOfKinName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NextOfKinPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("isVerified")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Resqu.Core.Entities.WalletPayment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerWalletId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ServiceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VendorId")
                        .HasColumnType("int");

                    b.Property<string>("VendorPhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorWalletId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VendorId");

                    b.ToTable("WalletPayments");
                });

            modelBuilder.Entity("Resqu.Core.Entities.CashPayment", b =>
                {
                    b.HasOne("Resqu.Core.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Resqu.Core.Entities.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId");

                    b.Navigation("Customer");

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Expertise", b =>
                {
                    b.HasOne("Resqu.Core.Entities.ExpertiseCategory", "GetExpertiseCategory")
                        .WithMany()
                        .HasForeignKey("ExpertiseCategoryId");

                    b.Navigation("GetExpertiseCategory");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Otp", b =>
                {
                    b.HasOne("Resqu.Core.Entities.Customer", "GetCustomer")
                        .WithMany()
                        .HasForeignKey("GetCustomerId");

                    b.Navigation("GetCustomer");
                });

            modelBuilder.Entity("Resqu.Core.Entities.Request", b =>
                {
                    b.HasOne("Resqu.Core.Entities.ExpertiseCategory", "GetExpertiseCategory")
                        .WithMany()
                        .HasForeignKey("GetExpertiseCategoryId");

                    b.HasOne("Resqu.Core.Entities.Expertise", "GetExpertise")
                        .WithMany()
                        .HasForeignKey("GetExpertiseId");

                    b.Navigation("GetExpertise");

                    b.Navigation("GetExpertiseCategory");
                });

            modelBuilder.Entity("Resqu.Core.Entities.ResquProcess", b =>
                {
                    b.HasOne("Resqu.Core.Entities.Customer", "GetCustomer")
                        .WithMany()
                        .HasForeignKey("GetCustomerId");

                    b.HasOne("Resqu.Core.Entities.Request", "GetRequest")
                        .WithMany()
                        .HasForeignKey("GetRequestId");

                    b.HasOne("Resqu.Core.Entities.Vendor", "GetVendor")
                        .WithMany()
                        .HasForeignKey("GetVendorId");

                    b.Navigation("GetCustomer");

                    b.Navigation("GetRequest");

                    b.Navigation("GetVendor");
                });

            modelBuilder.Entity("Resqu.Core.Entities.WalletPayment", b =>
                {
                    b.HasOne("Resqu.Core.Entities.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("Resqu.Core.Entities.Vendor", "Vendor")
                        .WithMany()
                        .HasForeignKey("VendorId");

                    b.Navigation("Customer");

                    b.Navigation("Vendor");
                });
#pragma warning restore 612, 618
        }
    }
}
