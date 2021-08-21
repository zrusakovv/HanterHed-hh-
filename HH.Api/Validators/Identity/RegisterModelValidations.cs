using FluentValidation;
using HH.Identity.Models;

namespace HH.Api.Validators.Identity
{
    public class RegisterModelValidations: AbstractValidator<RegisterModel>
    {
        public RegisterModelValidations()
        {
            RuleFor(x=>x.FirstName)
                .NotNull().NotEmpty();
            
            RuleFor(x=>x.LastName)
                .NotNull().NotEmpty();
            
            RuleFor(x=>x.Username)
                .NotNull().NotEmpty();
            
            RuleFor(x=>x.Email)
                .NotNull().NotEmpty().EmailAddress();
            
            RuleFor(x=>x.Password)
                .NotNull().NotEmpty();
        }
    }
}