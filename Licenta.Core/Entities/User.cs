using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class User : IdentityUser<long>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string RefreshToken { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
