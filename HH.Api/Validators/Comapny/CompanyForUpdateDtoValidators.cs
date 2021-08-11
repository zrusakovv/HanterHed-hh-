using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class CompanyForUpdateDtoValidators : AbstractValidator<CompanyForUpdateDto>
    {
        public CompanyForUpdateDtoValidators()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().MinimumLength(4).MaximumLength(10);

            RuleFor(x => x.Address)
                .NotNull().NotEmpty().MinimumLength(4).MaximumLength(20);

            RuleFor(x => x.Country)
                .NotNull().NotEmpty();
        }
    }
}
