using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class VacancyCreationDtoValidations : AbstractValidator<VacancyForCreationDto>
    {
        public VacancyCreationDtoValidations()
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

