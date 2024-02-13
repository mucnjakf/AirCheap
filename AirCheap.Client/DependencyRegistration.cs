using AirCheap.Client.ApiServices;
using AirCheap.Client.Authentication;
using AirCheap.Client.DataTransferObjects;
using AirCheap.Client.Validation;
using Blazored.LocalStorage;
using Blazored.Modal;
using Blazored.Toast;
using FluentValidation;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace AirCheap.Client;

public static class DependencyRegistration
{
    public static void RegisterServices(this WebAssemblyHostBuilder builder)
    {
        IServiceCollection services = builder.Services;

        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

        services.AddBlazoredLocalStorage();
        services.AddBlazoredModal();
        services.AddBlazoredToast();

        services.AddAuthorizationCore();
        services.AddTransient<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

        services.AddTransient<IUserApiService, UserApiService>();
        services.AddTransient<IFlightApiService, FlightApiService>();

        services.AddTransient<IValidator<UserAuthenticationDto>, UserAuthenticationDtoValidator>();
        services.AddTransient<IValidator<UserInsertDto>, UserInsertDtoValidator>();
        services.AddTransient<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
        services.AddTransient<IValidator<UserUpdateUsernameDto>, UserUpdateUsernameDtoValidator>();
        services.AddTransient<IValidator<UserUpdatePasswordDto>, UserUpdatePasswordDtoValidator>();
        services.AddTransient<IValidator<FlightGetDto>, FlightGetDtoValidator>();
    }
}
