using AirCheap.Core.Models;
using AirCheap.Server.DataTransferObjects;

namespace AirCheap.Server.ApiServices;

public interface IUserApiService
{
    Task<ResultResponseDto<UserDetails>> AuthenticateUserAsync(UserAuthenticationDto userAuthenticationDto);

    ResultResponseDto<UserDetails> GetUsers();

    Task<ResultResponseDto<UserDetails>> GetUserAsync(string username);

    Task<EmptyResponseDto> InsertUserAsync(UserInsertDto userInsertDto);

    Task<EmptyResponseDto> UpdateUserAsync(UserUpdateDto userUpdateDto);

    Task<EmptyResponseDto> UpdateUserUsernameAsync(UserUpdateUsernameDto userUpdateUsernameDto);

    Task<EmptyResponseDto> UpdateUserPasswordAsync(UserUpdatePasswordDto userUpdatePasswordDto);

    Task<EmptyResponseDto> DeleteUserAsync(string username);
}