using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class Role : IdentityRole<long>
{
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
