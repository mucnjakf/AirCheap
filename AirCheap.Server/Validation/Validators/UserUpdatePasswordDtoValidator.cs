using AirCheap.Server.DataTransferObjects;
using FluentValidation;

namespace AirCheap.Server.Validation.Validators;

public class UserUpdatePasswordDtoValidator : AbstractValidator<UserUpdatePasswordDto>
{
    public UserUpdatePasswordDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is required.");

        RuleFor(x => x.NewPassword).NotEmpty().WithMessage("New password is required.");

        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required.");

        RuleFor(x => x.NewPassword).Equal(x => x.ConfirmPassword).WithMessage("New password and confirm password do not match.");
    }
}
