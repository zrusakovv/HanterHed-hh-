using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HH.Data.SqlServer
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "Vacancies",
                columns: table => new
                {
                    VacancyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequiredWorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Busyness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conditions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeySkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vacancies", x => x.VacancyId);
                    table.ForeignKey(
                        name: "FK_Vacancies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "CompanyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Summaries",
                columns: table => new
                {
                    SummaryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkExperience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KeySkills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnowledgeOfLanguages = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Summaries", x => x.SummaryId);
                    table.ForeignKey(
                        name: "FK_Summaries_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "CompanyId", "Address", "Country", "Description", "Name" },
                values: new object[,]
                {
                    { new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"), "Ростов-на-Дону Нансена 439", "Россия", "Самая лучшая Ай Ти компания в мире", "IT ООО Рожки да ножки" },
                    { new Guid("4bd0821c-1807-46ab-8094-0fca909f0317"), "Ростов-на-Дону Гребцовый переулок 28", "Россия", "Самая лучшая Ай Ти компания в мире", "IT ООО Гребцы и Галеры " }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "City", "Country", "Email", "Name", "Phone", "Photo" },
                values: new object[,]
                {
                    { new Guid("167fa0ee-d874-4e70-8ee1-622aa9e5aec9"), "Ростов-на-Дону", "Россия", "Ivanov@gmail.com", "Иванов Иван Иванович", "1234567890", "../img/.../photo" },
                    { new Guid("28e547ab-bb7c-4294-ad8b-ba262c8b8a05"), "Ростов-на-Дону", "Россия", "Aleks@gmail.com", "Александров Александ Александрович", "1234567890", "../img/.../photo" }
                });

            migrationBuilder.InsertData(
                table: "Summaries",
                columns: new[] { "SummaryId", "AboutMe", "Citizenship", "City", "DateOfBirth", "Education", "Email", "EmployeeId", "FirstName", "Gender", "KeySkills", "KnowledgeOfLanguages", "LastName", "Phone", "Photo", "Position", "WorkExperience" },
                values: new object[,]
                {
                    { new Guid("aaff566a-6be1-4bec-ad1d-f35dbd077ed7"), "Люблю кодить", "Россия", "Ростов-на-Дону", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DGTU", "Ivanov@gmail.com", new Guid("167fa0ee-d874-4e70-8ee1-622aa9e5aec9"), "Иван", "Мужчина", "С#", "Русский, Английский  ", "Иванов", "123456789", "../photo/..", "C#", "2 года" },
                    { new Guid("fce4f6ef-8363-4d1d-ae1b-c464469a2d73"), "Люблю кодить", "Россия", "Ростов-на-Дону", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DGTU", "Alex@gmail.com", new Guid("28e547ab-bb7c-4294-ad8b-ba262c8b8a05"), "Александ", "Мужчина", "С#", "Русский, Английский  ", "Александров", "123456789", "../photo/..", "C# and Angular", "3 года" }
                });

            migrationBuilder.InsertData(
                table: "Vacancies",
                columns: new[] { "VacancyId", "Address", "Busyness", "CompanyDescription", "CompanyId", "Conditions", "KeySkills", "Name", "Price", "RequiredWorkExperience", "Requirements" },
                values: new object[,]
                {
                    { new Guid("a376a25b-9278-4a7f-9e31-cfec1d3709d7"), "Ростов-на-Дону Нансена 439", "Полный рабочий день", "......", new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"), "кофе и печеньки ", "C#", "Начинающий C# разработчик", 30000, "Без опыта", "C#" },
                    { new Guid("3224d364-db5c-4b3e-bc87-ffc0ef96c537"), "Ростов-на-Дону Нансена 439", "Полный рабочий день", "......", new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"), "кофе и печеньки ", "C#, Angular", "Начинающий C#и Angular разработчик", 30000, "Без опыта", "C#, Angular" },
                    { new Guid("5ed834ec-3a56-45af-b078-53aae28c6d7f"), "Ростов-на-Дону Нансена 439", "Полный рабочий день", "......", new Guid("ac620851-d101-4dab-9cd8-58a5ad4db2c4"), "кофе и печеньки ", "Photoshopr", "Начинающий Дизайнер", 30000, "Без опыта", "Photoshopr" },
                    { new Guid("13ffc751-d54f-49c7-b408-d5f8ab4cde70"), "Ростов-на-Дону Гребцовый переулок 28", "Полный рабочий день", "......", new Guid("4bd0821c-1807-46ab-8094-0fca909f0317"), "кофе и печеньки ", ".Net", "Начинающий .Net разработчик", 30000, "Без опыта", ".Net" },
                    { new Guid("38f9793d-edfe-4a74-a01d-bf5c450f5a0e"), "Ростов-на-Дону Гребцовый переулок 28", "Полный рабочий день", "......", new Guid("4bd0821c-1807-46ab-8094-0fca909f0317"), "кофе и печеньки ", ".Net and Angular", "Начинающий .Net and Angular разработчик", 30000, "Без опыта", ".Net and Angular" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Summaries_EmployeeId",
                table: "Summaries",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_CompanyId",
                table: "Vacancies",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Summaries");

            migrationBuilder.DropTable(
                name: "Vacancies");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
