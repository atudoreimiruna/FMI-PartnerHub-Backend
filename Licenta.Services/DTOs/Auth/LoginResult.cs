namespace Licenta.Services.DTOs.Auth;

public class LoginResult
{
    public bool Success { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
