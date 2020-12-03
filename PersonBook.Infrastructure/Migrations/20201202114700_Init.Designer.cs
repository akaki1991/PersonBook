﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PersonBook.Infrastructure.Db;

namespace PersonBook.Infrastructure.Migrations
{
    [DbContext(typeof(PersonBookDbContext))]
    [Migration("20201202114700_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("PersonBook")
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PersonBook.Domain.CityAggregate.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("City","PersonBook");
                });

            modelBuilder.Entity("PersonBook.Domain.CityAggregate.ReadModels.CityReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AggregateRootId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CityReadModel","PersonBookReadModel");
                });

            modelBuilder.Entity("PersonBook.Domain.PersonAggregate.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonalNumber")
                        .IsUnique()
                        .HasFilter("[PersonalNumber] IS NOT NULL");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("PersonBook.Domain.PersonAggregate.PhoneNumber", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("PhoneNumber");
                });

            modelBuilder.Entity("PersonBook.Domain.PersonAggregate.ReadModels.PersonReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AggregateRootId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("BirthDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("CityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Gender")
                        .HasColumnType("tinyint");

                    b.Property<string>("HomePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobilePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OfficePhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PersonalNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhotoHeight")
                        .HasColumnType("int");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int");

                    b.Property<string>("PhotoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhotoWidth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PersonReadModel","PersonBookReadModel");
                });

            modelBuilder.Entity("PersonBook.Domain.PersonRelationAggregate.PersonRelationship", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("FirstPersonId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte>("PersonRelationshipType")
                        .HasColumnType("tinyint");

                    b.Property<int>("SecondPersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PersonRelationship","PersonBook");
                });

            modelBuilder.Entity("PersonBook.Domain.PersonRelationAggregate.ReaedModels.PersonRelationshipReadModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AggregateRootId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("FirstPersonId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<byte>("PersonRelationshipType")
                        .HasColumnType("tinyint");

                    b.Property<int>("SecondPersonId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PersonRelationShipReadModel","PersonBookReadModel");
                });

            modelBuilder.Entity("PersonBook.Domain.PhotoAggregate.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTimeOffset>("CreateDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("LastChangeDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Width")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Photo","PersonBook");
                });

            modelBuilder.Entity("PersonBook.Domain.PersonAggregate.Person", b =>
                {
                    b.OwnsOne("PersonBook.Domain.PersonAggregate.ValueObjects.Photo", "Photo", b1 =>
                        {
                            b1.Property<int>("PersonId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<int>("PhotoId")
                                .HasColumnType("int");

                            b1.Property<string>("Url")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });
                });

            modelBuilder.Entity("PersonBook.Domain.PersonAggregate.PhoneNumber", b =>
                {
                    b.HasOne("PersonBook.Domain.PersonAggregate.Person", "Person")
                        .WithMany("PhoneNumbers")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
