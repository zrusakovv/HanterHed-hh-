using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class CompanyDtoValidations : AbstractValidator<CompanyDto>
    {
        public CompanyDtoValidations()
        {
            RuleFor(x => x.Id)
                .NotNull();

            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(4).MaximumLength(10);

            RuleFor(x => x.Address)
                .NotEmpty().MinimumLength(4).MaximumLength(20);

            RuleFor(x => x.Country)
                .NotEmpty();
        }
    }
}
