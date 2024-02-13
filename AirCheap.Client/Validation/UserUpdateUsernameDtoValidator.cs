using AirCheap.Client.DataTransferObjects;
using FluentValidation;

namespace AirCheap.Client.Validation;

public class UserUpdateUsernameDtoValidator : AbstractValidator<UserUpdateUsernameDto>
{
    public UserUpdateUsernameDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.NewUsername).NotEmpty().WithMessage("New username is required.");
    }
}
