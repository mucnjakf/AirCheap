using AirCheap.Server.DataTransferObjects;

namespace AirCheap.Server.Validation;

public interface IUserDtoValidationService
{
    Task<Tuple<bool, IEnumerable<string>>> ValidateUserAuthenticationDto(UserAuthenticationDto userAuthenticationDto);

    Task<Tuple<bool, IEnumerable<string>>> ValidateUserInsertDto(UserInsertDto userInsertDto);

    Task<Tuple<bool, IEnumerable<string>>> ValidateUserUpdateDto(UserUpdateDto userUpdateDto);

    Task<Tuple<bool, IEnumerable<string>>> ValidateUserUpdatePasswordDto(UserUpdatePasswordDto userUpdatePasswordDto);

    Task<Tuple<bool, IEnumerable<string>>> ValidateUserUpdateUsernameDto(UserUpdateUsernameDto userUpdateUsernameDto);
}