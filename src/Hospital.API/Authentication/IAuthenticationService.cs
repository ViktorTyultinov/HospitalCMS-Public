namespace Hospital.API.Authentication;

public interface IAuthenticationService
{
    Task RegisterAsync(CreateUserDto user);
    Task<(string AccessToken, string RefreshToken)> LoginAsync(string username, string password);
    Task<string> RefreshAccessTokenAsync(string refreshToken, string username);
}