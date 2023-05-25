using Licenta.Core.Entities.Base;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class Event : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public long? PartnerId { get; set; }
    public virtual Partner Partner { get; set; }
    public virtual ICollection<File> Files { get; set; } = new List<File>();
}
