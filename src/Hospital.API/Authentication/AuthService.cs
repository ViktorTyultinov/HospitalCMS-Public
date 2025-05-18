using System.Security.Claims;
using Hospital.API.Authentication.PasswordHasher;
using Hospital.API.Authentication.TokenFactory;
using Hospital.Application.Interfaces;
using Hospital.Domain.Entities.Authentication;
using Hospital.Domain.Entities.Users;

namespace Hospital.API.Authentication;

public class AuthenticationService(IPasswordHasher hasher, ITokenFactory tokenFactory, IUnitOfWork unitOfWork) : IAuthenticationService
{
    public async Task RegisterAsync(CreateUserDto user)
    {
        if (await unitOfWork.Users.IsNameAlreadyUsed(user.Username))
            throw new Exception("Username already exists");

        var userEntity = new User
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            DateOfBirth = user.DoB,
            Gender = user.Gender,
            Username = user.Username,
            Password = hasher.Hash(user.Password),
            Email = user.Email
        };

        await unitOfWork.Users.AddAsync(userEntity);
        await unitOfWork.CompleteAsync();
    }

    public async Task<(string AccessToken, string RefreshToken)> LoginAsync(string username, string password)
    {
        var user = await unitOfWork.Users.GetUserByUsername(username)
                   ?? throw new Exception("Invalid credentials");

        if (!hasher.Verify(password, user.Password))
            throw new Exception("Invalid credentials");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
        };

        var accessToken = tokenFactory.GenerateAccessToken(claims);
        var refreshToken = tokenFactory.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(7)
        };

        await unitOfWork.RefreshTokens.AddAsync(refreshTokenEntity);

        await unitOfWork.CompleteAsync();

        return (accessToken, refreshToken);
    }
    public async Task<string> RefreshAccessTokenAsync(string refreshToken, string username)
    {
        var user = await unitOfWork.Users.GetUserByUsername(username) ?? throw new Exception("User not found");
        var token = await unitOfWork.RefreshTokens.GetByIdAsync(user.Id) ?? throw new Exception("No refresh token found");

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, username),
        };

        var newAccessToken = tokenFactory.GenerateAccessToken(claims);
        return newAccessToken;
    }
}