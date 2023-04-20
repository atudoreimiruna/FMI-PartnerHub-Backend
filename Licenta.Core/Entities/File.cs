using Licenta.Core.Entities.Base;

namespace Licenta.Core.Entities;

public class File : BaseEntity
{
    public string Name { get; set; }
    public string? Uri { get; set; }
    public long? PostId { get; set; }
    public Post Post { get; set; }
}
