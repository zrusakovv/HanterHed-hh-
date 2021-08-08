using HH.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HH.Data
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData
                (
                    new Employee
                    {
                        Id = new Guid("167fa0ee-d874-4e70-8ee1-622aa9e5aec9"),
                        Name = "Иванов Иван Иванович",
                        Email = "Ivanov@gmail.com",
                        City = "Ростов-на-Дону",
                        Country = "Россия",
                        Phone = "1234567890",
                        Photo = "../img/.../photo"
                    },
                    new Employee
                    {
                        Id = new Guid("28e547ab-bb7c-4294-ad8b-ba262c8b8a05"),
                        Name = "Александров Александ Александрович",
                        Email = "Aleks@gmail.com",
                        City = "Ростов-на-Дону",
                        Country = "Россия",
                        Phone = "1234567890",
                        Photo = "../img/.../photo"
                    }
                );
        }
    }
}
