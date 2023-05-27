using Microsoft.AspNetCore.Mvc;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Licenta.Core.Entities;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;

namespace Licenta.Api.Controllers;

[Route("api/auth")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ITokenHelper _tokenHelper;

    public AuthController(IAuthManager authManager, ITokenHelper tokenHelper, SignInManager<User> signInManager, UserManager<User> userManager)
    {
        _authManager = authManager;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenHelper = tokenHelper;
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

    [HttpPost]
   // [Authorize(AuthenticationSchemes = MicrosoftAccountDefaults.AuthenticationScheme)]
    public async Task<IActionResult> ExternalSignIn([FromQuery] string token)
    {
        var x = await _signInManager.GetExternalLoginInfoAsync();

        var res = await _signInManager.ExternalLoginSignInAsync(
            MicrosoftAccountDefaults.AuthenticationScheme,
            "miruna.atudorei@s.unibuc.ro",
            true);

        if (res.Succeeded)
        {
            // TODO: GENERATE ACCESS TOKEN
            return Ok();

        }
        var user = new User
        {
            Email = "miruna.atudorei@s.unibuc.ro",
            UserName = "miruna.atudorei@s.unibuc.ro"
        };
        var result = await _userManager.CreateAsync(user);

        var login = await _userManager.AddLoginAsync(user, new UserLoginInfo(MicrosoftAccountDefaults.AuthenticationScheme, "miruna.atudorei@s.unibuc.ro", MicrosoftAccountDefaults.AuthenticationScheme));
        var accessToken = await _tokenHelper.CreateAccessToken(user);

        return Ok(accessToken);
    }

    //[HttpPost("signin")]
    //public async Task<IActionResult> SignIn()
    //{
    //    var properties = new AuthenticationProperties
    //    {
    //        RedirectUri = "/auth/callback",
    //        Items =
    //        {
    //            { "Scheme", OpenIdConnectDefaults.AuthenticationScheme },
    //        },
    //    };

    //    return Challenge(properties);
    //}

    //[HttpGet("Callback")]
    //public IActionResult Callback()
    //{
    //    return Ok();
    //}

    //[HttpGet("SignOut")]
    //public IActionResult SignOut()
    //{
    //    return SignOut(
    //        new AuthenticationProperties { RedirectUri = "/" },
    //        CookieAuthenticationDefaults.AuthenticationScheme,
    //        OpenIdConnectDefaults.AuthenticationScheme);
    //}
}
