using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class CompanyCreationDtoValidations : AbstractValidator<CompanyForCreationDto>
    {
        public CompanyCreationDtoValidations()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(4).MaximumLength(10)
                .WithMessage($"Имя является оязательным полем минимальная длина составляет");

            RuleFor(x => x.Address)
                .NotEmpty().MinimumLength(4).MaximumLength(20)
                .WithMessage($"Адрес является оязательным полем");

            RuleFor(x => x.Country)
                .NotEmpty();

        }
    }
}

