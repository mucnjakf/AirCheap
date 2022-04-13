using AirCheap.Core.Exceptions;
using AirCheap.Core.Models;
using AirCheap.Core.Services;
using AirCheap.Server.DataTransferObjects;
using AirCheap.Server.Validation;
using AutoMapper;

namespace AirCheap.Server.ApiServices;

public class UserApiService : IUserApiService
{
    private readonly IMapper _mapper;
    private readonly IUserDtoValidationService _userDtoValidationService;
    private readonly IUserService _userService;

    public UserApiService(IMapper mapper, IUserDtoValidationService userDtoValidationService, IUserService userService)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userDtoValidationService = userDtoValidationService ?? throw new ArgumentNullException(nameof(userDtoValidationService));
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
    }

    public async Task<ResultResponseDto<UserDetails>> AuthenticateUserAsync(UserAuthenticationDto userAuthenticationDto)
    {
        (bool success, IEnumerable<string> errors) = await _userDtoValidationService.ValidateUserAuthenticationDto(userAuthenticationDto);

        if (!success)
        {
            return new ResultResponseDto<UserDetails>
            {
                Success = false,
                Errors = errors
            };
        }

        try
        {
            UserAuthenticate userAuthenticate = _mapper.Map<UserAuthenticate>(userAuthenticationDto);
            UserDetails userDetails = await _userService.AuthenticateUserAsync(userAuthenticate);

            return new ResultResponseDto<UserDetails>
            {
                Success = true,
                ObjectResult = userDetails
            };
        }
        catch (Exception e)
        {
            return new ResultResponseDto<UserDetails>
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }

    public ResultResponseDto<UserDetails> GetUsers()
    {
        IEnumerable<UserDetails> users = _userService.GetUsers();

        return new ResultResponseDto<UserDetails>
        {
            Success = true,
            CollectionResult = users
        };
    }

    public async Task<ResultResponseDto<UserDetails>> GetUserAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return new ResultResponseDto<UserDetails>
            {
                Success = false,
                Errors = new List<string> { "Username is required." }
            };
        }

        try
        {
            UserDetails userDetails = await _userService.GetUserAsync(username);

            return new ResultResponseDto<UserDetails>
            {
                Success = true,
                ObjectResult = userDetails
            };
        }
        catch (Exception e)
        {
            return new ResultResponseDto<UserDetails>
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }

    public async Task<EmptyResponseDto> InsertUserAsync(UserInsertDto userInsertDto)
    {
        (bool success, IEnumerable<string> errors) = await _userDtoValidationService.ValidateUserInsertDto(userInsertDto);

        if (!success)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = errors
            };
        }

        try
        {
            UserInsert userInsert = _mapper.Map<UserInsert>(userInsertDto);
            await _userService.InsertUserAsync(userInsert);

            return new EmptyResponseDto { Success = true };
        }
        catch (Exception e)
        {
            EmptyResponseDto emptyResponseDto = new() { Success = false };

            if (e is MultipleErrorsException errorsException)
            {
                emptyResponseDto.Errors = errorsException.Errors;
                return emptyResponseDto;
            }

            emptyResponseDto.Errors = new List<string> { e.Message };
            return emptyResponseDto;
        }
    }

    public async Task<EmptyResponseDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        (bool success, IEnumerable<string> errors) = await _userDtoValidationService.ValidateUserUpdateDto(userUpdateDto);

        if (!success)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = errors
            };
        }

        try
        {
            UserUpdate userUpdate = _mapper.Map<UserUpdate>(userUpdateDto);
            await _userService.UpdateUserAsync(userUpdate);

            return new EmptyResponseDto { Success = true };
        }
        catch (Exception e)
        {
            EmptyResponseDto emptyResponseDto = new() { Success = false };

            if (e is MultipleErrorsException errorsException)
            {
                emptyResponseDto.Errors = errorsException.Errors;
                return emptyResponseDto;
            }

            emptyResponseDto.Errors = new List<string> { e.Message };
            return emptyResponseDto;
        }
    }

    public async Task<EmptyResponseDto> UpdateUserUsernameAsync(UserUpdateUsernameDto userUpdateUsernameDto)
    {
        (bool success, IEnumerable<string> errors) = await _userDtoValidationService.ValidateUserUpdateUsernameDto(userUpdateUsernameDto);

        if (!success)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = errors
            };
        }

        try
        {
            UserUpdateUsername userUpdateUsername = _mapper.Map<UserUpdateUsername>(userUpdateUsernameDto);
            await _userService.UpdateUserUsernameAsync(userUpdateUsername);

            return new EmptyResponseDto { Success = true };
        }
        catch (Exception e)
        {
            EmptyResponseDto emptyResponseDto = new() { Success = false };

            if (e is MultipleErrorsException errorsException)
            {
                emptyResponseDto.Errors = errorsException.Errors;
                return emptyResponseDto;
            }

            emptyResponseDto.Errors = new List<string> { e.Message };
            return emptyResponseDto;
        }
    }

    public async Task<EmptyResponseDto> UpdateUserPasswordAsync(UserUpdatePasswordDto userUpdatePasswordDto)
    {
        (bool success, IEnumerable<string> errors) = await _userDtoValidationService.ValidateUserUpdatePasswordDto(userUpdatePasswordDto);

        if (!success)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = errors
            };
        }

        try
        {
            UserUpdatePassword userUpdatePassword = _mapper.Map<UserUpdatePassword>(userUpdatePasswordDto);
            await _userService.UpdateUserPasswordAsync(userUpdatePassword);

            return new EmptyResponseDto { Success = true };
        }
        catch (Exception e)
        {
            EmptyResponseDto emptyResponseDto = new() { Success = false };

            if (e is MultipleErrorsException errorsException)
            {
                emptyResponseDto.Errors = errorsException.Errors;
                return emptyResponseDto;
            }

            emptyResponseDto.Errors = new List<string> { e.Message };
            return emptyResponseDto;
        }
    }

    public async Task<EmptyResponseDto> DeleteUserAsync(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = new List<string> { "Username is required." }
            };
        }

        try
        {
            await _userService.DeleteUserAsync(username);

            return new EmptyResponseDto { Success = true };
        }
        catch (Exception e)
        {
            EmptyResponseDto emptyResponseDto = new() { Success = false };

            if (e is MultipleErrorsException errorsException)
            {
                emptyResponseDto.Errors = errorsException.Errors;
                return emptyResponseDto;
            }

            emptyResponseDto.Errors = new List<string> { e.Message };
            return emptyResponseDto;
        }
    }
}
