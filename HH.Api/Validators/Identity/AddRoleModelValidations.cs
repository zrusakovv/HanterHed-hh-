using FluentValidation;
using HH.Identity.Models;

namespace HH.Api.Validators.Identity
{
    public class AddRoleModelValidations: AbstractValidator<AddRoleModel>
    {
        public AddRoleModelValidations()
        {
            RuleFor(x => x.Email)
                .NotNull().NotEmpty().EmailAddress();
            
            RuleFor(x => x.Password)
                .NotNull().NotEmpty();
            
            RuleFor(x => x.Role)
                .NotNull().NotEmpty();
        }
    }
}