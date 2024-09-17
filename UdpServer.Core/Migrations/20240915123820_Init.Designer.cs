﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UdpServer.Core.Data.Source.Dal.DataContext;


#nullable disable

namespace UdpServer.Core.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240915123820_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UdpServer.Core.Data.Dto.ChatDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("UdpServer.Core.Data.Dto.MessageDto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("ChatDtoId")
                        .HasColumnType("bigint");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ChatDtoId");

                    b.ToTable("MessageDto");
                });

            modelBuilder.Entity("UdpServer.Core.Data.Dto.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<long?>("ChatDtoId")
                        .HasColumnType("bigint");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChatDtoId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UdpServer.Core.Data.Dto.MessageDto", b =>
                {
                    b.HasOne("UdpServer.Core.Data.Dto.ChatDto", null)
                        .WithMany("MessageHistory")
                        .HasForeignKey("ChatDtoId");
                });

            modelBuilder.Entity("UdpServer.Core.Data.Dto.UserDto", b =>
                {
                    b.HasOne("UdpServer.Core.Data.Dto.ChatDto", null)
                        .WithMany("UsersList")
                        .HasForeignKey("ChatDtoId");
                });

            modelBuilder.Entity("UdpServer.Core.Data.Dto.ChatDto", b =>
                {
                    b.Navigation("MessageHistory");

                    b.Navigation("UsersList");
                });
#pragma warning restore 612, 618
        }
    }
}
