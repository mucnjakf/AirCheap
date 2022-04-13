using AirCheap.Server.DataTransferObjects;
using FluentValidation;

namespace AirCheap.Server.Validation.Validators;

public class UserUpdateUsernameDtoValidator : AbstractValidator<UserUpdateUsernameDto>
{
    public UserUpdateUsernameDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.NewUsername).NotEmpty().WithMessage("New username is required.");
    }
}
