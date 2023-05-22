using Microsoft.AspNetCore.Mvc;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Licenta.Core.Entities;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace Licenta.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ITokenHelper _tokenHelper;

    public AuthController(IAuthManager authManager, ITokenHelper tokenHelper, SignInManager<User> signInManager, UserManager<User> userManager, IConfiguration configuration)
    {
        _authManager = authManager;
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _tokenHelper = tokenHelper;
    }

    [HttpPost("tokens")]
    public async Task<IActionResult> ReceiveTokens([FromBody] TokenRequestDTO model)
    {
        string accessToken = model.AccessToken;
        string refreshToken = model.RefreshToken;
        // Process the access token and refresh token
        // ...
        var tokenHandler = new JwtSecurityTokenHandler();

        // Decode the access token
        var decodedToken = tokenHandler.ReadJwtToken(accessToken);

        // Extract the desired information from the decoded token
        string name = decodedToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        string email = decodedToken.Claims.FirstOrDefault(c => c.Type == "email")?.Value;

        var user = new User
        {
            Email = email,
            UserName = email
        };

        var result = await _userManager.CreateAsync(user);

        // await _userManager.AddLoginAsync(user, new UserLoginInfo("provider", accessToken, "display name"));

        await _userManager.SetAuthenticationTokenAsync(user, "token provider", "token name", accessToken);

        return Ok();
    }

    [HttpPost("microsoft/login")]
    public async Task<IActionResult> LoginWithMicrosoft()
    {
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info == null)
        {
            return BadRequest("Error retrieving external login information");
        }

        var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false);
        if (result.Succeeded)
        {
            var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
            //var roles = await _userManager.GetRolesAsync(user);

            //var claims = new List<Claim>
            //{
            //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.Email, user.Email),
            //};

            //foreach (var role in roles)
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, role));
            //}

            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("+YIULoPaY3gTNe36xnq2GqefDxiDY1cAzmzINtf7zdE="));
            //var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    _configuration["Jwt:Issuer"],
            //    _configuration["Jwt:Audience"],
            //    claims,
            //    expires: DateTime.Now.AddMinutes(30),
            //    signingCredentials: creds
            //);

            var token = await _tokenHelper.CreateAccessToken(user);
            var refreshToken = _tokenHelper.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            return Ok(new LoginResult
            {
                Success = true,
                AccessToken = token,
                RefreshToken = refreshToken
            });
        }

        return BadRequest("Error logging in with Microsoft account");
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
    {
        var result = await _authManager.Register(registerModel);
        return result ? Ok(result) : BadRequest(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
    {
        var result = await _authManager.Login(loginModel);

        if (result.Success == true)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Failed to login");
        }
    }
    
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshModel refreshModel)
    {
        var result = await _authManager.Refresh(refreshModel);
        return !result.Contains("Bad") ? Ok(result) : BadRequest("Failed to refresh");
    }
}
