using System.Collections.Generic;
using System;

namespace Licenta.Core.Entities;

public class Partner
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Contact { get; set; }
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
}
