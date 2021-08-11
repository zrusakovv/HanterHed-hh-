using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class EmployeeForUpdateDtoValidators : AbstractValidator<EmployeeForUpdateDto>
    {
        public EmployeeForUpdateDtoValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty().MinimumLength(4).MaximumLength(10); ;

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.Country)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .NotEmpty().MinimumLength(10).MaximumLength(10); ;

            RuleFor(x => x.Photo)
                .NotEmpty();

        }
    }
}
