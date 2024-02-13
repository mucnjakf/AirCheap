using AirCheap.Client.Authentication;
using AirCheap.Client.DataTransferObjects;
using AirCheap.Client.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AirCheap.Client.ApiServices;

public class UserApiService : IUserApiService
{
    private readonly HttpClient _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _localStorageService;

    public UserApiService(
        HttpClient httpClient,
        AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService localStorage)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _authenticationStateProvider = authenticationStateProvider ?? throw new ArgumentNullException(nameof(authenticationStateProvider));
        _localStorageService = localStorage ?? throw new ArgumentNullException(nameof(localStorage));
    }

    public async Task<ResultResponseDto<UserDetails>> LoginUserAsync(UserAuthenticationDto userAuthenticationDto)
    {
        try
        {
            StringContent data = new(JsonSerializer.Serialize(userAuthenticationDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync("api/user/authentication", data);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            ResultResponseDto<UserDetails> response = JsonSerializer.Deserialize<ResultResponseDto<UserDetails>>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.Success)
            {
                return response;
            }

            await _localStorageService.SetItemAsync("token", response.ObjectResult.Token);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(userAuthenticationDto.Username);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.ObjectResult.Token);

            return response;
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

    public async Task LogoutUserAsync()
    {
        await _localStorageService.RemoveItemAsync("token");

        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();

        _httpClient.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<ResultResponseDto<UserDetails>> GetUserAsync(string username)
    {
        try
        {
            HttpResponseMessage httpMessage = await _httpClient.GetAsync($"api/user/{username}");

            string stringResponse = await httpMessage.Content.ReadAsStringAsync();
            ResultResponseDto<UserDetails> response = JsonSerializer.Deserialize<ResultResponseDto<UserDetails>>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
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
        try
        {
            StringContent data = new(JsonSerializer.Serialize(userInsertDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PostAsync("api/user", data);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            EmptyResponseDto response = JsonSerializer.Deserialize<EmptyResponseDto>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }
        catch (Exception e)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }

    public async Task<EmptyResponseDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
    {
        try
        {
            StringContent data = new(JsonSerializer.Serialize(userUpdateDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PutAsync("api/user", data);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            EmptyResponseDto response = JsonSerializer.Deserialize<EmptyResponseDto>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }
        catch (Exception e)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }

    public async Task<EmptyResponseDto> UpdateUserUsernameAsync(UserUpdateUsernameDto userUpdateUsernameDto)
    {
        try
        {
            StringContent data = new(JsonSerializer.Serialize(userUpdateUsernameDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PutAsync("api/user/username", data);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            EmptyResponseDto response = JsonSerializer.Deserialize<EmptyResponseDto>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }
        catch (Exception e)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }

    public async Task<EmptyResponseDto> UpdateUserPasswordAsync(UserUpdatePasswordDto userUpdatePasswordDto)
    {
        try
        {
            StringContent data = new(JsonSerializer.Serialize(userUpdatePasswordDto), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponse = await _httpClient.PutAsync("api/user/password", data);

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            EmptyResponseDto response = JsonSerializer.Deserialize<EmptyResponseDto>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }
        catch (Exception e)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }

    public async Task<EmptyResponseDto> DeleteUserAsync(string username)
    {
        try
        {
            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync($"api/user/{username}");

            string stringResponse = await httpResponse.Content.ReadAsStringAsync();
            EmptyResponseDto response = JsonSerializer.Deserialize<EmptyResponseDto>(stringResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }
        catch (Exception e)
        {
            return new EmptyResponseDto
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };
        }
    }
}
