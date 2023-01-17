﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using RemoteFinder.DAL;

#nullable disable

namespace RemoteFinder.DAL.Migrations
{
    [DbContext(typeof(MainContext))]
    [Migration("20230116121359_BookPreviewImageNullable")]
    partial class BookPreviewImageNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("RemoteFinder.Entities.Authentication.UserAdminEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("LoginsCount")
                        .HasColumnType("integer");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UsersAdmin", (string)null);
                });

            modelBuilder.Entity("RemoteFinder.Entities.Authentication.UserSocialEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<int>("OAuthProvider")
                        .HasColumnType("integer");

                    b.Property<string>("ProviderUserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserPicture")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UsersSocial", (string)null);
                });

            modelBuilder.Entity("RemoteFinder.Entities.Payments.PaymentEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("OrderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("UserSocialId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserSocialId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("RemoteFinder.Entities.Storage.BookEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<int>("FileId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PreviewImageUrl")
                        .HasColumnType("text");

                    b.Property<int>("UserSocialId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("FileId");

                    b.HasIndex("UserSocialId");

                    b.ToTable("Books", (string)null);
                });

            modelBuilder.Entity("RemoteFinder.Entities.Storage.CategoryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserSocialId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserSocialId");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("RemoteFinder.Entities.Storage.FileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("text");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("FileSize")
                        .HasColumnType("numeric");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("UserSocialId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserSocialId");

                    b.ToTable("Files", (string)null);
                });

            modelBuilder.Entity("RemoteFinder.Entities.Payments.PaymentEntity", b =>
                {
                    b.HasOne("RemoteFinder.Entities.Authentication.UserSocialEntity", "UserSocial")
                        .WithMany("Payments")
                        .HasForeignKey("UserSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSocial");
                });

            modelBuilder.Entity("RemoteFinder.Entities.Storage.BookEntity", b =>
                {
                    b.HasOne("RemoteFinder.Entities.Storage.CategoryEntity", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RemoteFinder.Entities.Storage.FileEntity", "File")
                        .WithMany()
                        .HasForeignKey("FileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RemoteFinder.Entities.Authentication.UserSocialEntity", "UserSocial")
                        .WithMany()
                        .HasForeignKey("UserSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("File");

                    b.Navigation("UserSocial");
                });

            modelBuilder.Entity("RemoteFinder.Entities.Storage.CategoryEntity", b =>
                {
                    b.HasOne("RemoteFinder.Entities.Authentication.UserSocialEntity", "UserSocial")
                        .WithMany()
                        .HasForeignKey("UserSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSocial");
                });

            modelBuilder.Entity("RemoteFinder.Entities.Storage.FileEntity", b =>
                {
                    b.HasOne("RemoteFinder.Entities.Authentication.UserSocialEntity", "UserSocial")
                        .WithMany("Files")
                        .HasForeignKey("UserSocialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserSocial");
                });

            modelBuilder.Entity("RemoteFinder.Entities.Authentication.UserSocialEntity", b =>
                {
                    b.Navigation("Files");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}