using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Core.Entities;

public class User : IdentityUser<long>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? LastUpdated { get; set; }
    public string RefreshToken { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; }
}
