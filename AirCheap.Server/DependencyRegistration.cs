using AirCheap.Core.Repositories;
using AirCheap.Core.Services;
using AirCheap.DAL;
using AirCheap.DAL.Entities;
using AirCheap.DAL.Repositories;
using AirCheap.Server.ApiServices;
using AirCheap.Server.DataTransferObjects;
using AirCheap.Server.Validation;
using AirCheap.Server.Validation.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AirCheap.Server;

public static class DependencyRegistration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        IConfiguration configuration = builder.Configuration;
        IServiceCollection services = builder.Services;

        services.AddControllersWithViews();
        services.AddRazorPages();

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DevConnection")));

        services.AddDefaultIdentity<UserEntity>().AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JwtAuthentication:ValidIssuer"],
                ValidAudience = configuration["JwtAuthentication:ValidAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtAuthentication:Secret"]))
            };
        });

        services.AddAutoMapper(typeof(AutoMapperProfile));

        services.AddTransient<IValidator<UserAuthenticationDto>, UserAuthenticationDtoValidator>();
        services.AddTransient<IValidator<UserInsertDto>, UserInsertDtoValidator>();
        services.AddTransient<IValidator<UserUpdateDto>, UserUpdateDtoValidator>();
        services.AddTransient<IValidator<UserUpdateUsernameDto>, UserUpdateUsernameDtoValidator>();
        services.AddTransient<IValidator<UserUpdatePasswordDto>, UserUpdatePasswordDtoValidator>();
        services.AddTransient<IValidator<FlightGetDto>, FlightGetDtoValidator>();

        services.AddTransient<IUserDtoValidationService, UserDtoValidationService>();
        services.AddTransient<IFlightDtoValidationService, FlightDtoValidationService>();

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IFlightRepository, FlightRepository>();

        services.AddTransient<IUserService, UserService>();
        services.AddTransient<IFlightService, FlightService>();

        services.AddTransient<IUserApiService, UserApiService>();
        services.AddTransient<IFlightApiService, FlightApiService>();
    }
}
