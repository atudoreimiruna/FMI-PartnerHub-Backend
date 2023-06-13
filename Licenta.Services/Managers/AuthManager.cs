using Microsoft.AspNetCore.Identity;
using Licenta.Core.Entities;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.Identity.Web;
using Licenta.Core.Enums;
using Licenta.Services.DTOs.Student;
using AutoMapper;
using Licenta.Core.Interfaces;
using Licenta.Core.Extensions.PagedList;
using Licenta.Services.QueryParameters;
using Licenta.Services.Specifications;
using System.Linq;

namespace Licenta.Services.Managers;

public class AuthManager : IAuthManager
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly ITokenHelper _tokenHelper;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<UserRole> _userRoleRepository;
    private readonly IMapper _mapper;

    public AuthManager(UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<Role> roleManager,
        IRepository<Student> studentRepository,
        IRepository<User> userRepository,
        IRepository<UserRole> userRoleRepository,
        IMapper mapper,
        ITokenHelper tokenHelper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenHelper = tokenHelper;
        _roleManager = roleManager;
        _studentRepository = studentRepository;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _mapper = mapper;
    }

    public async Task<LoginResult> Login(LoginModel loginModel)
    {
        var user = await _userManager.FindByEmailAsync(loginModel.Email);
        if (user == null)
        {
            user = new User
            {
                Email = loginModel.Email,
                UserName = loginModel.Email
            };
            var userResult = await _userManager.CreateAsync(user);
            if (!userResult.Succeeded)
            {
                return new LoginResult
                {
                    Success = false
                };
            }
            else
            {
                await _userManager.AddToRoleAsync(user, RolesEnum.User.ToString());

                var studentDto = new StudentPostDTO
                {
                    Email = loginModel.Email,
                    Name = loginModel.Name
                };
                var student = _mapper.Map<Student>(studentDto);

                await _studentRepository.AddAsync(student);
            }
        }

        var result = await _signInManager.ExternalLoginSignInAsync(
            Constants.AzureAd,
            loginModel.Email,
            true);

        if (result.Succeeded)
        {
            var accessToken = await _tokenHelper.CreateAccessToken(user);
            var refreshToken = _tokenHelper.CreateRefreshToken();
            return new LoginResult
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        else
        {
            await _userManager.AddLoginAsync(user, new UserLoginInfo(Constants.AzureAd, loginModel.Email, Constants.AzureAd));
            var accessToken = await _tokenHelper.CreateAccessToken(user);
            var refreshToken = _tokenHelper.CreateRefreshToken();
            return new LoginResult
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }

    public async Task<PagedList<UserViewDTO>> ListUsersAsync(UserParameters parameters)
    {
        var spec = new UserSpecification(parameters);
        var users = await _userRepository.FindBySpecAsync(spec);
        return _mapper.Map<PagedList<UserViewDTO>>(users);
    }

    public async Task<bool> AddRoleToUserAsync(RegisterModel registerModel)
    {
        var user = await _userManager.FindByEmailAsync(registerModel.Email);
        if (user == null)
        {
            user = new User
            {
                Email = registerModel.Email,
                UserName = registerModel.Email
            };
            var userResult = await _userManager.CreateAsync(user);
            if (!userResult.Succeeded)
            {
                return false;
            }
        }

        // TODO: verificare daca user ul exista in entitatea student si daca da, trebuie sters
        var roleExists = await _roleManager.RoleExistsAsync(registerModel.Role.ToString());
        if (!roleExists)
        {
            return false; 
        }

        var result = await _userManager.AddToRoleAsync(user, registerModel.Role.ToString());
        return result.Succeeded;
    }

    public async Task<bool> AddPartnerToAdminAsync(AdminPartnerDTO adminPartnerDTO)
    {
        var user = await _userManager.FindByEmailAsync(adminPartnerDTO.Email);
        if (user == null)
        {
            return false;
        }

        var roleExists = await _roleManager.RoleExistsAsync(RolesEnum.Admin.ToString());
        if (!roleExists)
        {
            return false;
        }
        else
        {
            var userRole = _userRoleRepository
                .AsQueryable()
                .Where(x => x.UserId == user.Id && x.RoleId == 2)
                .FirstOrDefault();

            if (userRole == null) { return false; }
            else 
            { 
                userRole.PartnerId = adminPartnerDTO.PartnerId;
                await _userRoleRepository.UpdateAsync(userRole);
                return true;
            }
        }
    }


    public async Task<bool> RemoveRoleFromUserAsync(RegisterModel registerModel)
    {
        var user = await _userManager.FindByEmailAsync(registerModel.Email);
        if (user == null)
        {
            return false;
        }

        var roleExists = await _roleManager.RoleExistsAsync(registerModel.Role.ToString());
        if (!roleExists)
        {
            return false;
        }

        var result = await _userManager.RemoveFromRoleAsync(user, registerModel.Role.ToString());
        return result.Succeeded;
    }


    public async Task<string> Refresh(RefreshModel refreshModel)
    {
        var principal = _tokenHelper.GetPrincipalFromExpiredToken(refreshModel.AccessToken);
        var username = principal.Identity.Name;

        var user = await _userManager.FindByEmailAsync(username);

        if (user.RefreshToken != refreshModel.RefreshToken)
        {
            return "Bad Refresh";
        }

        var newJwtToken = await _tokenHelper.CreateAccessToken(user);
        return newJwtToken;
    }
}
