using Licenta.Core.Entities.Base;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class Partner : BaseEntity
{
    public string Name { get; set; }
    public string MainDescription { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Social { get; set; }
    public string MainImageUrl { get; set; }
    public string LogoImageUrl { get; set; }
    public string ProfileImageUrl { get; set; }
    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    public virtual ICollection<Job> Jobs { get; set; } = new List<Job>();
    public virtual ICollection<File> Files { get; set; } = new List<File>();
}
