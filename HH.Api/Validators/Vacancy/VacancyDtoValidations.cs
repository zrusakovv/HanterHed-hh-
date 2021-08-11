using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class VacancyDtoValidations : AbstractValidator<VacancyDto>
    {
        public VacancyDtoValidations()
        {
            RuleFor(x => x.Id)
                .NotNull().NotEmpty();

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
