﻿// <auto-generated />
using System;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Migrations
{
    [DbContext(typeof(SQLHoshinCoreContext))]
    [Migration("20181011130747_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-preview2-35157")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Jobs", b =>
                {
                    b.Property<int>("JobID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Active");

                    b.Property<string>("JobTitle");

                    b.Property<string>("Nomenclature");

                    b.HasKey("JobID");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.JobsSectorsPlants", b =>
                {
                    b.Property<int>("JobID");

                    b.Property<int>("PlantID");

                    b.Property<int>("SectorID");

                    b.Property<int>("JobPlantSupID");

                    b.Property<int>("JobSectorSupID");

                    b.Property<int>("JobSupID");

                    b.HasKey("JobID", "PlantID", "SectorID");

                    b.HasIndex("SectorID", "PlantID");

                    b.ToTable("JobsSectorsPlants");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Plants", b =>
                {
                    b.Property<int>("PlantID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("PlantID");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Roles", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Sectors", b =>
                {
                    b.Property<int>("SectorID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("SectorID");

                    b.ToTable("Sectors");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.SectorsPlants", b =>
                {
                    b.Property<int>("PlantID");

                    b.Property<int>("SectorID");

                    b.Property<int>("ReferringJob");

                    b.Property<int>("ReferringJob2");

                    b.HasKey("PlantID", "SectorID");

                    b.HasIndex("SectorID");

                    b.ToTable("SectorsPlants");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Users", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<int>("JobID");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MicrosoftGraphId");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("PlantID");

                    b.Property<int>("SectorID");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("Status");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("SectorID", "PlantID", "JobID");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserToken", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.JobsSectorsPlants", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Jobs", "Job")
                        .WithMany("JobsSectorsPlants")
                        .HasForeignKey("JobID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.SectorsPlants", "SectorPlant")
                        .WithMany("JobsSectorsPlants")
                        .HasForeignKey("SectorID", "PlantID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.RoleClaim", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Roles", "Role")
                        .WithMany("RoleClaims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.SectorsPlants", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Plants", "Plant")
                        .WithMany("SectorsPlants")
                        .HasForeignKey("PlantID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Sectors", "Sector")
                        .WithMany("SectorsPlants")
                        .HasForeignKey("SectorID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserClaim", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Users", "User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserLogin", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Users", "User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserRole", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Users", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Users", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.JobsSectorsPlants", "JobSectorPlant")
                        .WithMany("Users")
                        .HasForeignKey("SectorID", "PlantID", "JobID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.UserToken", b =>
                {
                    b.HasOne("Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Users", "User")
                        .WithMany("Tokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}