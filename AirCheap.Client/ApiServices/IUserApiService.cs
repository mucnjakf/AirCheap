using AirCheap.Client.DataTransferObjects;
using AirCheap.Client.Models;

namespace AirCheap.Client.ApiServices;

public interface IUserApiService
{
    Task<ResultResponseDto<UserDetails>> LoginUserAsync(UserAuthenticationDto userAuthenticationDto);

    Task LogoutUserAsync();

    Task<ResultResponseDto<UserDetails>> GetUserAsync(string username);

    Task<EmptyResponseDto> InsertUserAsync(UserInsertDto userInsertDto);

    Task<EmptyResponseDto> UpdateUserAsync(UserUpdateDto userUpdateDto);

    Task<EmptyResponseDto> UpdateUserUsernameAsync(UserUpdateUsernameDto userUpdateUsernameDto);

    Task<EmptyResponseDto> UpdateUserPasswordAsync(UserUpdatePasswordDto userUpdatePasswordDto);

    Task<EmptyResponseDto> DeleteUserAsync(string username);
}