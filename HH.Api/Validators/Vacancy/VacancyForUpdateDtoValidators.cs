using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class VacancyForUpdateDtoValidators: AbstractValidator<VacancyForUpdateDto>
    {
        public VacancyForUpdateDtoValidators()
        {
            RuleFor(x => x.Name)
                 .NotNull().NotEmpty();

            RuleFor(x => x.Price)
                .NotNull().NotEmpty();

            RuleFor(x => x.Address)
                .NotNull().NotEmpty();

            RuleFor(x => x.Busyness)
                .NotNull().NotEmpty();
        }
    }
}
