using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.QueryParameters;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IAuthManager
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task<string> Refresh(RefreshModel refreshModel);
    Task<bool> AddRoleToUserAsync(RegisterModel registerModel);
    Task<bool> RemoveRoleFromUserAsync(RegisterModel registerModel);
    Task<PagedList<UserViewDTO>> ListUsersAsync(UserParameters parameters);
    Task<bool> AddPartnerToAdminAsync(AdminPartnerDTO adminPartnerDTO);
}
