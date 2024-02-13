using AirCheap.Server.DataTransferObjects;
using FluentValidation;
using FluentValidation.Results;

namespace AirCheap.Server.Validation;

public class UserDtoValidationService : IUserDtoValidationService
{
    private readonly IValidator<UserAuthenticationDto> _userAuthenticationDtoValidator;
    private readonly IValidator<UserInsertDto> _userInsertDtoValidator;
    private readonly IValidator<UserUpdateDto> _userUpdateDtoValidator;
    private readonly IValidator<UserUpdateUsernameDto> _userUpdateUsernameDtoValidator;
    private readonly IValidator<UserUpdatePasswordDto> _userUpdatePasswordDtoValidator;

    public UserDtoValidationService(
        IValidator<UserAuthenticationDto> userAuthenticationDtoValidator,
        IValidator<UserInsertDto> userInsertDtoValidator,
        IValidator<UserUpdateDto> userUpdateDtoValidator,
        IValidator<UserUpdateUsernameDto> userUpdateUsernameDtoValidator,
        IValidator<UserUpdatePasswordDto> userUpdatePasswordDtoValidator)
    {
        _userAuthenticationDtoValidator = userAuthenticationDtoValidator ?? throw new ArgumentNullException(nameof(userAuthenticationDtoValidator));
        _userInsertDtoValidator = userInsertDtoValidator ?? throw new ArgumentNullException(nameof(userInsertDtoValidator));
        _userUpdateDtoValidator = userUpdateDtoValidator ?? throw new ArgumentNullException(nameof(userUpdateDtoValidator));
        _userUpdateUsernameDtoValidator = userUpdateUsernameDtoValidator ?? throw new ArgumentNullException(nameof(userUpdateUsernameDtoValidator));
        _userUpdatePasswordDtoValidator = userUpdatePasswordDtoValidator ?? throw new ArgumentNullException(nameof(userUpdatePasswordDtoValidator));
    }

    public async Task<Tuple<bool, IEnumerable<string>>> ValidateUserAuthenticationDto(UserAuthenticationDto userAuthenticationDto)
    {
        ValidationResult result = await _userAuthenticationDtoValidator.ValidateAsync(userAuthenticationDto);

        return result.IsValid
            ? new Tuple<bool, IEnumerable<string>>(true, null)
            : new Tuple<bool, IEnumerable<string>>(false, result.Errors.Select(error => error.ErrorMessage));
    }

    public async Task<Tuple<bool, IEnumerable<string>>> ValidateUserInsertDto(UserInsertDto userInsertDto)
    {
        ValidationResult result = await _userInsertDtoValidator.ValidateAsync(userInsertDto);

        return result.IsValid
            ? new Tuple<bool, IEnumerable<string>>(true, null)
            : new Tuple<bool, IEnumerable<string>>(false, result.Errors.Select(error => error.ErrorMessage));
    }

    public async Task<Tuple<bool, IEnumerable<string>>> ValidateUserUpdateDto(UserUpdateDto userUpdateDto)
    {
        ValidationResult result = await _userUpdateDtoValidator.ValidateAsync(userUpdateDto);

        return result.IsValid
            ? new Tuple<bool, IEnumerable<string>>(true, null)
            : new Tuple<bool, IEnumerable<string>>(false, result.Errors.Select(error => error.ErrorMessage));
    }

    public async Task<Tuple<bool, IEnumerable<string>>> ValidateUserUpdateUsernameDto(UserUpdateUsernameDto userUpdateUsernameDto)
    {
        ValidationResult result = await _userUpdateUsernameDtoValidator.ValidateAsync(userUpdateUsernameDto);

        return result.IsValid
            ? new Tuple<bool, IEnumerable<string>>(true, null)
            : new Tuple<bool, IEnumerable<string>>(false, result.Errors.Select(error => error.ErrorMessage));
    }

    public async Task<Tuple<bool, IEnumerable<string>>> ValidateUserUpdatePasswordDto(UserUpdatePasswordDto userUpdatePasswordDto)
    {
        ValidationResult result = await _userUpdatePasswordDtoValidator.ValidateAsync(userUpdatePasswordDto);

        return result.IsValid
            ? new Tuple<bool, IEnumerable<string>>(true, null)
            : new Tuple<bool, IEnumerable<string>>(false, result.Errors.Select(error => error.ErrorMessage));
    }
}
