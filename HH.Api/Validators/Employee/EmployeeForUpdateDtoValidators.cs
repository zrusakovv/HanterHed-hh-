using FluentValidation;
using HH.DTO;

namespace HanterHed_hh_.Validators
{
    public class EmployeeForUpdateDtoValidators : AbstractValidator<EmployeeForUpdateDto>
    {
        public EmployeeForUpdateDtoValidators()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.Country)
                .NotEmpty();

            RuleFor(x => x.Phone)
                .NotEmpty();

            RuleFor(x => x.Photo)
                .NotEmpty();
        }
    }
}
