using Licenta.Core.Enums;

namespace Licenta.Services.DTOs.Auth;

public class RegisterModel
{
    public string Email { get; set; }
    public RolesEnum Role { get; set; }
}
