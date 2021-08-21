using FluentValidation;
using HH.Identity.Models;

namespace HH.Api.Validators.Identity
{
    public class TokenRequestModelValidations: AbstractValidator<TokenRequestModel>
    {
        public TokenRequestModelValidations()
        {
            RuleFor(x=>x.Email)
                .NotNull().NotEmpty().EmailAddress();

            RuleFor(x => x.Password)
                .NotNull().NotEmpty();
        }
    }
}