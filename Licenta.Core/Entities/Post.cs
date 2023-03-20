using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class Post
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public long? PartnerId { get; set; }
    public virtual Partner Partner { get; set; }
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
