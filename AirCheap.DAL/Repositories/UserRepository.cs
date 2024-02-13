using AirCheap.Core.Exceptions;
using AirCheap.Core.Models;
using AirCheap.Core.Repositories;
using AirCheap.DAL.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AirCheap.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;

    public UserRepository(
        IConfiguration configuration,
        IMapper mapper,
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
    }

    public async Task<UserDetails> AuthenticateUserAsync(UserAuthenticate userAuthenticate)
    {
        UserEntity userEntity = await _userManager.FindByNameAsync(userAuthenticate.Username);

        if (userEntity is null)
        {
            throw new Exception($"Username {userAuthenticate.Username} not found.");
        }

        SignInResult signInResult = await _signInManager.PasswordSignInAsync(userEntity, userAuthenticate.Password, false, false);

        if (!signInResult.Succeeded)
        {
            throw new Exception($"Password is invalid.");
        }

        List<Claim> claims = new() { new Claim(ClaimTypes.Name, userEntity.UserName) };

        string issuer = _configuration["JwtAuthentication:ValidIssuer"];
        string audience = _configuration["JwtAuthentication:ValidAudience"];

        SymmetricSecurityKey secret = new(Encoding.UTF8.GetBytes(_configuration["JwtAuthentication:Secret"]));
        SigningCredentials signingCredentials = new(secret, SecurityAlgorithms.HmacSha256);
        DateTime expires = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtAuthentication:ExpiryInDays"]));

        JwtSecurityToken jwtToken = new(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expires,
            signingCredentials: signingCredentials
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        UserDetails userDetails = _mapper.Map<UserDetails>(userEntity);
        userDetails.Token = token;

        return userDetails;
    }

    public IEnumerable<UserDetails> GetUsers()
    {
        IQueryable<UserEntity> userEntities = _userManager.Users.Where(userEntity => !userEntity.IsDeleted);
        IEnumerable<UserDetails> users = _mapper.Map<IEnumerable<UserDetails>>(userEntities);
        return users;
    }

    public async Task<UserDetails> GetUserAsync(string username)
    {
        UserEntity userEntity = await _userManager.FindByNameAsync(username);

        if (userEntity is null || userEntity.IsDeleted)
        {
            throw new Exception($"User with username {username} not found.");
        }

        UserDetails userDetails = _mapper.Map<UserDetails>(userEntity);
        return userDetails;
    }

    public async Task InsertUserAsync(UserInsert userInsert)
    {
        UserEntity userEntity = _mapper.Map<UserEntity>(userInsert);

        IdentityResult identityResult = await _userManager.CreateAsync(userEntity, userInsert.Password);

        if (!identityResult.Succeeded)
        {
            List<string> errors = identityResult.Errors.Select(error => error.Description).ToList();
            throw new MultipleErrorsException(errors);
        }
    }

    public async Task UpdateUserAsync(UserUpdate userUpdate)
    {
        UserEntity userEntity = await _userManager.FindByNameAsync(userUpdate.Username);

        if (userEntity is null || userEntity.IsDeleted)
        {
            throw new Exception($"User with username {userUpdate.Username} not found.");
        }

        userEntity.FirstName = userUpdate.FirstName;
        userEntity.LastName = userUpdate.LastName;

        IdentityResult identityResult = await _userManager.UpdateAsync(userEntity);

        if (!identityResult.Succeeded)
        {
            List<string> errors = identityResult.Errors.Select(error => error.Description).ToList();
            throw new MultipleErrorsException(errors);
        }

        await UpdateUserEmailAsync(userUpdate.Username, userUpdate.Email);
    }

    private async Task UpdateUserEmailAsync(string username, string email)
    {
        UserEntity userEntity = await _userManager.FindByNameAsync(username);

        string emailChangeToken = await _userManager.GenerateChangeEmailTokenAsync(userEntity, email);

        IdentityResult identityResult = await _userManager.ChangeEmailAsync(userEntity, email, emailChangeToken);

        if (!identityResult.Succeeded)
        {
            List<string> errors = identityResult.Errors.Select(error => error.Description).ToList();
            throw new MultipleErrorsException(errors);
        }
    }

    public async Task UpdateUserUsernameAsync(UserUpdateUsername userUpdateUsername)
    {
        UserEntity userEntity = await _userManager.FindByNameAsync(userUpdateUsername.Username);

        if (userEntity is null || userEntity.IsDeleted)
        {
            throw new Exception($"User with username {userUpdateUsername.Username} not found.");
        }

        IdentityResult identityResult = await _userManager.SetUserNameAsync(userEntity, userUpdateUsername.NewUsername);

        if (!identityResult.Succeeded)
        {
            List<string> errors = identityResult.Errors.Select(error => error.Description).ToList();
            throw new MultipleErrorsException(errors);
        }
    }

    public async Task UpdateUserPasswordAsync(UserUpdatePassword userUpdatePassword)
    {
        UserEntity userEntity = await _userManager.FindByNameAsync(userUpdatePassword.Username);

        if (userEntity is null || userEntity.IsDeleted)
        {
            throw new Exception($"User with username {userUpdatePassword.Username} not found.");
        }

        IdentityResult identityResult = await _userManager.ChangePasswordAsync(
            userEntity, userUpdatePassword.CurrentPassword, userUpdatePassword.NewPassword);

        if (!identityResult.Succeeded)
        {
            List<string> errors = identityResult.Errors.Select(error => error.Description).ToList();
            throw new MultipleErrorsException(errors);
        }
    }

    public async Task DeleteUserAsync(string username)
    {
        Guid deleteGuid = Guid.NewGuid();
        string deletedUser = $"DELETED_USER_{deleteGuid}";

        UserEntity userEntity = await _userManager.FindByNameAsync(username);

        if (userEntity is null)
        {
            throw new Exception($"User with username {username} not found.");
        }

        userEntity.UserName = deletedUser;
        userEntity.NormalizedEmail = deletedUser;
        userEntity.Email = deletedUser;
        userEntity.NormalizedEmail = deletedUser;
        userEntity.PasswordHash = deleteGuid.ToString();
        userEntity.SecurityStamp = deleteGuid.ToString();
        userEntity.PhoneNumber = deletedUser;
        userEntity.IsDeleted = true;

        IdentityResult identityResult = await _userManager.UpdateAsync(userEntity);

        if (!identityResult.Succeeded)
        {
            List<string> errors = identityResult.Errors.Select(error => error.Description).ToList();
            throw new MultipleErrorsException(errors);
        }
    }
}
