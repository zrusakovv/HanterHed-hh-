using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class VacancyConfiguration : IEntityTypeConfiguration<Vacancy>
    {
        public void Configure(EntityTypeBuilder<Vacancy> builder)
        {
            builder.HasData
                (
                    new Vacancy
                    {
                        Id = new Guid("a376a25b-9278-4a7f-9e31-cfec1d3709d7"),
                        Name = "Начинающий C# разработчик",
                        Price = 30000,
                        Address = "Ростов-на-Дону Нансена 439",
                        RequiredWorkExperience = "Без опыта",
                        Busyness = "Полный рабочий день",
                        CompanyDescription = "......",
                        Requirements = "C#",
                        Conditions = "кофе и печеньки ",
                        KeySkills = "C#",
                        CompanyId = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4")
                    },
                    new Vacancy
                    {
                        Id = new Guid("3224d364-db5c-4b3e-bc87-ffc0ef96c537"),
                        Name = "Начинающий C#и Angular разработчик",
                        Price = 30000,
                        Address = "Ростов-на-Дону Нансена 439",
                        RequiredWorkExperience = "Без опыта",
                        Busyness = "Полный рабочий день",
                        CompanyDescription = "......",
                        Requirements = "C#, Angular",
                        Conditions = "кофе и печеньки ",
                        KeySkills = "C#, Angular",
                        CompanyId = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4")
                    },
                    new Vacancy
                    {
                        Id = new Guid("5ed834ec-3a56-45af-b078-53aae28c6d7f"),
                        Name = "Начинающий Дизайнер",
                        Price = 30000,
                        Address = "Ростов-на-Дону Нансена 439",
                        RequiredWorkExperience = "Без опыта",
                        Busyness = "Полный рабочий день",
                        CompanyDescription = "......",
                        Requirements = "Photoshopr",
                        Conditions = "кофе и печеньки ",
                        KeySkills = "Photoshopr",
                        CompanyId = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4")
                    },
                    new Vacancy
                    {
                        Id = new Guid("13ffc751-d54f-49c7-b408-d5f8ab4cde70"),
                        Name = "Начинающий .Net разработчик",
                        Price = 30000,
                        Address = "Ростов-на-Дону Гребцовый переулок 28",
                        RequiredWorkExperience = "Без опыта",
                        Busyness = "Полный рабочий день",
                        CompanyDescription = "......",
                        Requirements = ".Net",
                        Conditions = "кофе и печеньки ",
                        KeySkills = ".Net",
                        CompanyId = new Guid("4bd0821c-1807-46ab-8094-0fca909f0317")
                    },
                    new Vacancy
                    {
                        Id = new Guid("38f9793d-edfe-4a74-a01d-bf5c450f5a0e"),
                        Name = "Начинающий .Net and Angular разработчик",
                        Price = 30000,
                        Address = "Ростов-на-Дону Гребцовый переулок 28",
                        RequiredWorkExperience = "Без опыта",
                        Busyness = "Полный рабочий день",
                        CompanyDescription = "......",
                        Requirements = ".Net and Angular",
                        Conditions = "кофе и печеньки ",
                        KeySkills = ".Net and Angular",
                        CompanyId = new Guid("4bd0821c-1807-46ab-8094-0fca909f0317")
                    }
                );
        }
    }
}
