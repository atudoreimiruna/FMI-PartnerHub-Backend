using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class User : IdentityUser<int>
{
    public string RefreshToken { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
