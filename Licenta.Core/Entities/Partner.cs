using Licenta.Core.Entities.Base;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class Partner : BaseEntity
{
    public string Name { get; set; }
    public string MainDescription { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Contact { get; set; }
    public string MainImageUrl { get; set; }
    public string LogoImageUrl { get; set; }
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
}
