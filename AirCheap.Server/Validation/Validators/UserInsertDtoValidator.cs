using AirCheap.Server.DataTransferObjects;
using FluentValidation;

namespace AirCheap.Server.Validation.Validators;

public class UserInsertDtoValidator : AbstractValidator<UserInsertDto>
{
    public UserInsertDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");

        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("E-Mail is required.");

        RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Confirm password is required.");

        RuleFor(x => x.Password).Equal(x => x.ConfirmPassword).WithMessage("Password and Confirm password do not match.");
    }
}