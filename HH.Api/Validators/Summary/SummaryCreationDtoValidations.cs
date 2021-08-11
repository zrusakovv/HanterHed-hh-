using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class SummaryCreationDtoValidations : AbstractValidator<SummaryForCreationDto>
    {
        public SummaryCreationDtoValidations()
        {
            RuleFor(x => x.Photo)
                .NotNull().NotEmpty();

            RuleFor(x => x.FirstName)
                .NotNull().NotEmpty().MinimumLength(4).MaximumLength(10);

            RuleFor(x => x.LastName)
                .NotNull().NotEmpty().MinimumLength(4).MaximumLength(10);

            RuleFor(x => x.Phone)
                .NotNull().NotEmpty().MinimumLength(10).MaximumLength(10);

        }
    }
}
