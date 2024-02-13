using AirCheap.Core.Models;
using AirCheap.Core.Repositories;

namespace AirCheap.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<UserDetails> AuthenticateUserAsync(UserAuthenticate userAuthenticate)
        => await _userRepository.AuthenticateUserAsync(userAuthenticate);

    public IEnumerable<UserDetails> GetUsers()
        => _userRepository.GetUsers();

    public async Task<UserDetails> GetUserAsync(string username)
        => await _userRepository.GetUserAsync(username);

    public async Task InsertUserAsync(UserInsert userInsert)
        => await _userRepository.InsertUserAsync(userInsert);

    public async Task UpdateUserAsync(UserUpdate userUpdate)
        => await _userRepository.UpdateUserAsync(userUpdate);

    public async Task UpdateUserUsernameAsync(UserUpdateUsername userUpdateUsername)
        => await _userRepository.UpdateUserUsernameAsync(userUpdateUsername);

    public async Task UpdateUserPasswordAsync(UserUpdatePassword userUpdatePassword)
        => await _userRepository.UpdateUserPasswordAsync(userUpdatePassword);

    public async Task DeleteUserAsync(string username)
        => await _userRepository.DeleteUserAsync(username);
}
