using HH.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HH.Data
{
    public class SummaryConfiguration : IEntityTypeConfiguration<Summary>
    {
        public void Configure(EntityTypeBuilder<Summary> builder)
        {
            builder.HasData
                (
                    new Summary
                    {
                        Id = new Guid("aaff566a-6be1-4bec-ad1d-f35dbd077ed7"),
                        Photo = "../photo/..",
                        FirstName = "Иван",
                        LastName = "Иванов",
                        Phone = "123456789",
                        Email = "Ivanov@gmail.com",
                        City = "Ростов-на-Дону",
                        DateOfBirth = new DateTime(),
                        Gender = "Мужчина",
                        Citizenship = "Россия",
                        Position = "C#",
                        WorkExperience = "2 года",
                        KeySkills = "С#",
                        AboutMe = "Люблю кодить",
                        Education = "DGTU",
                        KnowledgeOfLanguages = "Русский, Английский  ",
                        EmployeeId = new Guid("167fa0ee-d874-4e70-8ee1-622aa9e5aec9")
                    },
                    new Summary
                    {
                        Id = new Guid("fce4f6ef-8363-4d1d-ae1b-c464469a2d73"),
                        Photo = "../photo/..",
                        FirstName = "Александ",
                        LastName = "Александров",
                        Phone = "123456789",
                        Email = "Alex@gmail.com",
                        City = "Ростов-на-Дону",
                        DateOfBirth = new DateTime(),
                        Gender = "Мужчина",
                        Citizenship = "Россия",
                        Position = "C# and Angular",
                        WorkExperience = "3 года",
                        KeySkills = "С#",
                        AboutMe = "Люблю кодить",
                        Education = "DGTU",
                        KnowledgeOfLanguages = "Русский, Английский  ",
                        EmployeeId = new Guid("28e547ab-bb7c-4294-ad8b-ba262c8b8a05")
                    }
                );
        }
    }
}
