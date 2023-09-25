using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Licenta.Core.Entities;

public class UserRole : IdentityUserRole<long>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? LastUpdated { get; set; }
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
    public long PartnerId { get; set; }
}
