using System.Collections.Generic;

namespace Licenta.Services.DTOs.Auth;

public class UserViewDTO
{
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
}
