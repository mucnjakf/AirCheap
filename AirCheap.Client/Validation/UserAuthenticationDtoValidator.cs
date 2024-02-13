using AirCheap.Client.DataTransferObjects;
using FluentValidation;

namespace AirCheap.Client.Validation;

public class UserAuthenticationDtoValidator : AbstractValidator<UserAuthenticationDto>
{
    public UserAuthenticationDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}
