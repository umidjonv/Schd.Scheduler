﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Schd.Scheduler.Data;

#nullable disable

namespace Schd.Scheduler.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Schd.Data.Entity.AdImages", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AdId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("ADS_IMAGES");
                });

            modelBuilder.Entity("Schd.Data.Entity.Ads", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("ADS");
                });

            modelBuilder.Entity("Schd.Data.Entity.Channels", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("CHANNELS");
                });

            modelBuilder.Entity("Schd.Data.Entity.Owners", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("OWNERS");
                });

            modelBuilder.Entity("Schd.Data.Entity.OwnerTariff", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("OWNER_TARIFF");
                });

            modelBuilder.Entity("Schd.Data.Entity.ScheduledAds", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("AdId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsComplited")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<long>("SchedulerId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.HasIndex("SchedulerId");

                    b.ToTable("SCHEDULED_ADS");
                });

            modelBuilder.Entity("Schd.Data.Entity.ScheduleTemplates", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<string[]>("CustomDates")
                        .HasColumnType("text[]");

                    b.Property<string[]>("CustomTimes")
                        .HasColumnType("text[]");

                    b.Property<bool>("Enabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("RegularityDays")
                        .HasColumnType("integer");

                    b.Property<int>("RegularityHours")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("SCHEDULE_TEMPLATES");
                });

            modelBuilder.Entity("Schd.Data.Entity.Statistics", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<int>("Incomed")
                        .HasColumnType("integer");

                    b.Property<int>("Leaved")
                        .HasColumnType("integer");

                    b.Property<DateTime>("RevisionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UsersCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("STATISTICS");
                });

            modelBuilder.Entity("Schd.Data.Entity.Tariff", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedIp")
                        .HasColumnType("text");

                    b.Property<long>("CurrencyId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ModifiedIp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("TARIFF");
                });

            modelBuilder.Entity("Schd.Data.Entity.AdImages", b =>
                {
                    b.HasOne("Schd.Data.Entity.Ads", "Ads")
                        .WithMany("AdImages")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ads");
                });

            modelBuilder.Entity("Schd.Data.Entity.Ads", b =>
                {
                    b.HasOne("Schd.Data.Entity.Owners", "Owners")
                        .WithMany("Ads")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owners");
                });

            modelBuilder.Entity("Schd.Data.Entity.Channels", b =>
                {
                    b.HasOne("Schd.Data.Entity.Owners", "Owners")
                        .WithMany("Channels")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owners");
                });

            modelBuilder.Entity("Schd.Data.Entity.OwnerTariff", b =>
                {
                    b.HasOne("Schd.Data.Entity.Owners", "Owners")
                        .WithMany("OwnerTariffs")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owners");
                });

            modelBuilder.Entity("Schd.Data.Entity.ScheduledAds", b =>
                {
                    b.HasOne("Schd.Data.Entity.Ads", "Ads")
                        .WithMany("ScheduledAds")
                        .HasForeignKey("AdId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Schd.Data.Entity.ScheduleTemplates", "ScheduleTemplates")
                        .WithMany("ScheduledAds")
                        .HasForeignKey("SchedulerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ads");

                    b.Navigation("ScheduleTemplates");
                });

            modelBuilder.Entity("Schd.Data.Entity.Ads", b =>
                {
                    b.Navigation("AdImages");

                    b.Navigation("ScheduledAds");
                });

            modelBuilder.Entity("Schd.Data.Entity.Owners", b =>
                {
                    b.Navigation("Ads");

                    b.Navigation("Channels");

                    b.Navigation("OwnerTariffs");
                });

            modelBuilder.Entity("Schd.Data.Entity.ScheduleTemplates", b =>
                {
                    b.Navigation("ScheduledAds");
                });
#pragma warning restore 612, 618
        }
    }
}