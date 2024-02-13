using AirCheap.Core.Models;

namespace AirCheap.Core.Services;

public interface IUserService
{
    Task<UserDetails> AuthenticateUserAsync(UserAuthenticate userAuthenticate);

    IEnumerable<UserDetails> GetUsers();

    Task<UserDetails> GetUserAsync(string username);

    Task InsertUserAsync(UserInsert userInsert);

    Task UpdateUserAsync(UserUpdate userUpdate);

    Task UpdateUserUsernameAsync(UserUpdateUsername userUpdateUsername);

    Task UpdateUserPasswordAsync(UserUpdatePassword userUpdatePassword);

    Task DeleteUserAsync(string username);
}
