using Licenta.Services.DTOs.Auth;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IAuthManager
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task<string> Refresh(RefreshModel refreshModel);
}
