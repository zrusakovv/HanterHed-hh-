using System;
using HH.Core;
using Xunit;
using AutoFixture.Xunit2;
using AutoMapper;
using FluentAssertions;
using HH.DTO;
using HH.Infrastructure.Mapper;

namespace HH.UnitTests.MappingTests
{
    public class CompanyMappingTests
    {
        [Theory(DisplayName = "Описание теста"), AutoData]
        public void CompanyDtoToCompanyMappingTest(CompanyForCreationDto dto)
        {
            // Arrange
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(typeof(CompanyMappingProfile))).CreateMapper();

            Func<Company, bool> condition = x =>
                !string.IsNullOrEmpty(x.Name) &&
                !string.IsNullOrEmpty(x.Address) &&
                !string.IsNullOrEmpty(x.Country) &&
                !string.IsNullOrEmpty(x.Description);
            
            // Act
            var company = mapper.Map<Company>(dto);

            // Assert
            condition(company).Should().BeTrue();
        }
    }
}