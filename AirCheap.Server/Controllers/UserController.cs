using AirCheap.Core.Models;
using AirCheap.Server.ApiServices;
using AirCheap.Server.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AirCheap.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserApiService _userApiService;

    public UserController(IUserApiService userApiService)
    {
        this._userApiService = userApiService ?? throw new ArgumentNullException(nameof(userApiService));
    }

    [AllowAnonymous]
    [HttpPost("authentication")]
    public async Task<ResultResponseDto<UserDetails>> AuthenticateUserAsync([FromBody] UserAuthenticationDto userAuthenticationDto)
        => await _userApiService.AuthenticateUserAsync(userAuthenticationDto);

    [HttpGet]
    public ResultResponseDto<UserDetails> GetUsers()
    => _userApiService.GetUsers();

    [HttpGet("{username}")]
    public async Task<ResultResponseDto<UserDetails>> GetUser([FromRoute] string username)
        => await _userApiService.GetUserAsync(username);

    [AllowAnonymous]
    [HttpPost]
    public async Task<EmptyResponseDto> InsertUserAsync([FromBody] UserInsertDto userInsertDto)
        => await _userApiService.InsertUserAsync(userInsertDto);

    [HttpPut]
    public async Task<EmptyResponseDto> UpdateUserAsync([FromBody] UserUpdateDto userUpdateDto)
        => await _userApiService.UpdateUserAsync(userUpdateDto);

    [HttpPut("username")]
    public async Task<EmptyResponseDto> UpdateUserUsernameAsync([FromBody] UserUpdateUsernameDto userUpdateUsernameDto)
        => await _userApiService.UpdateUserUsernameAsync(userUpdateUsernameDto);

    [HttpPut("password")]
    public async Task<EmptyResponseDto> UpdateUserPasswordAsync([FromBody] UserUpdatePasswordDto userUpdatePasswordDto)
        => await _userApiService.UpdateUserPasswordAsync(userUpdatePasswordDto);

    [HttpDelete("{username}")]
    public async Task<EmptyResponseDto> DeleteUserAsync([FromRoute] string username)
        => await _userApiService.DeleteUserAsync(username);
}