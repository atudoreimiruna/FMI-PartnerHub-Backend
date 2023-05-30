using Microsoft.AspNetCore.Identity;
using System;

namespace Licenta.Core.Entities;

public class UserRole : IdentityUserRole<long>
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}
