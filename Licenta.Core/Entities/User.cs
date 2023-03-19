using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class User : IdentityUser<long>
{
    public string RefreshToken { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
