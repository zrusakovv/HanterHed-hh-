using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Entities.Configuration
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData
                (
                    new Company
                    {
                        Id = new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"),
                        Name = "IT ООО Рожки да ножки",
                        Address = "Ростов-на-Дону Нансена 439",
                        Country = "Россия",
                        Description = "Самая лучшая Ай Ти компания в мире"
                    },
                    new Company
                    {
                        Id = new Guid("4bd0821c-1807-46ab-8094-0fca909f0317"),
                        Name = "IT ООО Гребцы и Галеры ",
                        Address = "Ростов-на-Дону Гребцовый переулок 28",
                        Country = "Россия",
                        Description = "Самая лучшая Ай Ти компания в мире"
                    }
                );
        }
    }
}
