﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HanterHed_hh_.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("CompanyId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Companies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"),
                            Address = "Ростов-на-Дону Нансена 439",
                            Country = "Россия",
                            Description = "Самая лучшая Ай Ти компания в мире",
                            Name = "IT ООО Рожки да ножки"
                        },
                        new
                        {
                            Id = new Guid("4bd0821c-1807-46ab-8094-0fca909f0317"),
                            Address = "Ростов-на-Дону Гребцовый переулок 28",
                            Country = "Россия",
                            Description = "Самая лучшая Ай Ти компания в мире",
                            Name = "IT ООО Гребцы и Галеры "
                        });
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("EmployeeId");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = new Guid("167fa0ee-d874-4e70-8ee1-622aa9e5aec9"),
                            City = "Ростов-на-Дону",
                            Country = "Россия",
                            Email = "Ivanov@gmail.com",
                            Name = "Иванов Иван Иванович",
                            Phone = "1234567890",
                            Photo = "../img/.../photo"
                        },
                        new
                        {
                            Id = new Guid("28e547ab-bb7c-4294-ad8b-ba262c8b8a05"),
                            City = "Ростов-на-Дону",
                            Country = "Россия",
                            Email = "Aleks@gmail.com",
                            Name = "Александров Александ Александрович",
                            Phone = "1234567890",
                            Photo = "../img/.../photo"
                        });
                });

            modelBuilder.Entity("Entities.Models.Summary", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("SummaryId");

                    b.Property<string>("AboutMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Citizenship")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Education")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("EmployeeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeySkills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KnowledgeOfLanguages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Photo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WorkExperience")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Summaries");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aaff566a-6be1-4bec-ad1d-f35dbd077ed7"),
                            AboutMe = "Люблю кодить",
                            Citizenship = "Россия",
                            City = "Ростов-на-Дону",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Education = "DGTU",
                            Email = "Ivanov@gmail.com",
                            EmployeeId = new Guid("167fa0ee-d874-4e70-8ee1-622aa9e5aec9"),
                            FirstName = "Иван",
                            Gender = "Мужчина",
                            KeySkills = "С#",
                            KnowledgeOfLanguages = "Русский, Английский  ",
                            LastName = "Иванов",
                            Phone = "123456789",
                            Photo = "../photo/..",
                            Position = "C#",
                            WorkExperience = "2 года"
                        },
                        new
                        {
                            Id = new Guid("fce4f6ef-8363-4d1d-ae1b-c464469a2d73"),
                            AboutMe = "Люблю кодить",
                            Citizenship = "Россия",
                            City = "Ростов-на-Дону",
                            DateOfBirth = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Education = "DGTU",
                            Email = "Alex@gmail.com",
                            EmployeeId = new Guid("28e547ab-bb7c-4294-ad8b-ba262c8b8a05"),
                            FirstName = "Александ",
                            Gender = "Мужчина",
                            KeySkills = "С#",
                            KnowledgeOfLanguages = "Русский, Английский  ",
                            LastName = "Александров",
                            Phone = "123456789",
                            Photo = "../photo/..",
                            Position = "C# and Angular",
                            WorkExperience = "3 года"
                        });
                });

            modelBuilder.Entity("Entities.Models.Vacancy", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("VacancyId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Busyness")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CompanyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Conditions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("KeySkills")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("RequiredWorkExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Requirements")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Vacancies");

                    b.HasData(
                        new
                        {
                            Id = new Guid("a376a25b-9278-4a7f-9e31-cfec1d3709d7"),
                            Address = "Ростов-на-Дону Нансена 439",
                            Busyness = "Полный рабочий день",
                            CompanyDescription = "......",
                            CompanyId = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"),
                            Conditions = "кофе и печеньки ",
                            KeySkills = "C#",
                            Name = "Начинающий C# разработчик",
                            Price = 30000,
                            RequiredWorkExperience = "Без опыта",
                            Requirements = "C#"
                        },
                        new
                        {
                            Id = new Guid("3224d364-db5c-4b3e-bc87-ffc0ef96c537"),
                            Address = "Ростов-на-Дону Нансена 439",
                            Busyness = "Полный рабочий день",
                            CompanyDescription = "......",
                            CompanyId = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"),
                            Conditions = "кофе и печеньки ",
                            KeySkills = "C#, Angular",
                            Name = "Начинающий C#и Angular разработчик",
                            Price = 30000,
                            RequiredWorkExperience = "Без опыта",
                            Requirements = "C#, Angular"
                        },
                        new
                        {
                            Id = new Guid("5ed834ec-3a56-45af-b078-53aae28c6d7f"),
                            Address = "Ростов-на-Дону Нансена 439",
                            Busyness = "Полный рабочий день",
                            CompanyDescription = "......",
                            CompanyId = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"),
                            Conditions = "кофе и печеньки ",
                            KeySkills = "Photoshopr",
                            Name = "Начинающий Дизайнер",
                            Price = 30000,
                            RequiredWorkExperience = "Без опыта",
                            Requirements = "Photoshopr"
                        },
                        new
                        {
                            Id = new Guid("13ffc751-d54f-49c7-b408-d5f8ab4cde70"),
                            Address = "Ростов-на-Дону Гребцовый переулок 28",
                            Busyness = "Полный рабочий день",
                            CompanyDescription = "......",
                            CompanyId = new Guid("4bd0821c-1807-46ab-8094-0fca909f0317"),
                            Conditions = "кофе и печеньки ",
                            KeySkills = ".Net",
                            Name = "Начинающий .Net разработчик",
                            Price = 30000,
                            RequiredWorkExperience = "Без опыта",
                            Requirements = ".Net"
                        },
                        new
                        {
                            Id = new Guid("38f9793d-edfe-4a74-a01d-bf5c450f5a0e"),
                            Address = "Ростов-на-Дону Гребцовый переулок 28",
                            Busyness = "Полный рабочий день",
                            CompanyDescription = "......",
                            CompanyId = new Guid("4bd0821c-1807-46ab-8094-0fca909f0317"),
                            Conditions = "кофе и печеньки ",
                            KeySkills = ".Net and Angular",
                            Name = "Начинающий .Net and Angular разработчик",
                            Price = 30000,
                            RequiredWorkExperience = "Без опыта",
                            Requirements = ".Net and Angular"
                        });
                });

            modelBuilder.Entity("Entities.Models.Summary", b =>
                {
                    b.HasOne("Entities.Models.Employee", "Employee")
                        .WithMany("Summaries")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Entities.Models.Vacancy", b =>
                {
                    b.HasOne("Entities.Models.Company", "Company")
                        .WithMany("Vacancies")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("Entities.Models.Company", b =>
                {
                    b.Navigation("Vacancies");
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.Navigation("Summaries");
                });
#pragma warning restore 612, 618
        }
    }
}