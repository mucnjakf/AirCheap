using AirCheap.Client.DataTransferObjects;
using FluentValidation;

namespace AirCheap.Client.Validation;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");

        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");

        RuleFor(x => x.Email).NotEmpty().WithMessage("E-Mail is required");
    }
}
