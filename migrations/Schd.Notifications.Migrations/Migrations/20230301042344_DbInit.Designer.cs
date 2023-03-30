﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Schd.Notification.Data;

#nullable disable

namespace Schd.Notifications.Migrations.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230301042344_DbInit")]
    partial class DbInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Schd.Notification.Data.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Secret")
                        .HasColumnType("text");

                    b.Property<string>("WebUrl")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("clients");
                });

            modelBuilder.Entity("Schd.Notification.Data.Command", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("commands");
                });

            modelBuilder.Entity("Schd.Notification.Data.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("events");
                });

            modelBuilder.Entity("Schd.Notification.Data.Log", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("logs");
                });

            modelBuilder.Entity("Schd.Notification.Data.State", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CommandId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LogId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CommandId");

                    b.HasIndex("EventId");

                    b.HasIndex("LogId");

                    b.ToTable("states");
                });

            modelBuilder.Entity("Schd.Notification.Data.StateHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CommandId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("LogId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("CommandId");

                    b.HasIndex("EventId");

                    b.HasIndex("LogId");

                    b.ToTable("states_history");
                });

            modelBuilder.Entity("Schd.Notification.Data.Command", b =>
                {
                    b.HasOne("Schd.Notification.Data.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Schd.Notification.Data.Event", b =>
                {
                    b.HasOne("Schd.Notification.Data.Client", "Client")
                        .WithMany("Events")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Schd.Notification.Data.Log", b =>
                {
                    b.HasOne("Schd.Notification.Data.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Schd.Notification.Data.State", b =>
                {
                    b.HasOne("Schd.Notification.Data.Client", "Client")
                        .WithMany("States")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Schd.Notification.Data.Command", null)
                        .WithMany("States")
                        .HasForeignKey("CommandId");

                    b.HasOne("Schd.Notification.Data.Event", "Event")
                        .WithMany("States")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Schd.Notification.Data.Log", null)
                        .WithMany("States")
                        .HasForeignKey("LogId");

                    b.Navigation("Client");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Schd.Notification.Data.StateHistory", b =>
                {
                    b.HasOne("Schd.Notification.Data.Client", "Client")
                        .WithMany("StateHistories")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Schd.Notification.Data.Command", null)
                        .WithMany("StateHistories")
                        .HasForeignKey("CommandId");

                    b.HasOne("Schd.Notification.Data.Event", "Event")
                        .WithMany("StateHistories")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Schd.Notification.Data.Log", null)
                        .WithMany("StateHistories")
                        .HasForeignKey("LogId");

                    b.Navigation("Client");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("Schd.Notification.Data.Client", b =>
                {
                    b.Navigation("Events");

                    b.Navigation("StateHistories");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Schd.Notification.Data.Command", b =>
                {
                    b.Navigation("StateHistories");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Schd.Notification.Data.Event", b =>
                {
                    b.Navigation("StateHistories");

                    b.Navigation("States");
                });

            modelBuilder.Entity("Schd.Notification.Data.Log", b =>
                {
                    b.Navigation("StateHistories");

                    b.Navigation("States");
                });
#pragma warning restore 612, 618
        }
    }
}