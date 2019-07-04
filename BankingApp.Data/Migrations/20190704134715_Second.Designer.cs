﻿// <auto-generated />
using System;
using BankingApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BankingApp.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20190704134715_Second")]
    partial class Second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BankingApp.Data.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Amount");

                    b.Property<Guid?>("ReceiverUserId");

                    b.Property<Guid?>("SenderUserId");

                    b.Property<DateTimeOffset>("TimeOfTransaction");

                    b.Property<int>("TransactionType");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverUserId");

                    b.HasIndex("SenderUserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BankingApp.Data.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Balance");

                    b.Property<string>("Email");

                    b.Property<string>("Login");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BankingApp.Data.Entities.Transaction", b =>
                {
                    b.HasOne("BankingApp.Data.Entities.User", "ReceiverUser")
                        .WithMany("ReceivedTransactions")
                        .HasForeignKey("ReceiverUserId");

                    b.HasOne("BankingApp.Data.Entities.User", "SenderUser")
                        .WithMany("SentTransactions")
                        .HasForeignKey("SenderUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
