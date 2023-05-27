using Microsoft.AspNetCore.Identity;
using Licenta.Core.Entities;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class AuthManager : IAuthManager
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ITokenHelper _tokenHelper;

    public AuthManager(UserManager<User> userManager,
        SignInManager<User> signInManager,
        ITokenHelper tokenHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHelper = tokenHelper;
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if (user == null)
            return new LoginResult
            {
                Success = false
            };
        else
        {
            //var result = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
            //if (result.Succeeded)
            //{
                var token = await _tokenHelper.CreateAccessToken(user);
                var refreshToken = _tokenHelper.CreateRefreshToken();

                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);

                return new LoginResult
                {
                    Success = true,
                    AccessToken = token,
                    RefreshToken = refreshToken
                };
            //}
            //else
            //    return new LoginResult
            //    {
            //        Success = false
            //    };
        }

    }

    public async Task<bool> Register(RegisterModel registerModel)
    {
        var user = new User
        {
            Email = registerModel.Email,
            UserName = registerModel.Email
        };

        var result = await _userManager.CreateAsync(user);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, registerModel.Role);
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<string> Refresh(RefreshModel refreshModel)
    {
        var principal = _tokenHelper.GetPrincipalFromExpiredToken(refreshModel.AccessToken);
        var username = principal.Identity.Name;

        var user = await _userManager.FindByEmailAsync(username);

        if (user.RefreshToken != refreshModel.RefreshToken)
            return "Bad Refresh";

        var newJwtToken = await _tokenHelper.CreateAccessToken(user);

        return newJwtToken;
    }
}
