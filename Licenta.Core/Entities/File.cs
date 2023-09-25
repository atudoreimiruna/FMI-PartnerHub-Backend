using Licenta.Core.Entities.Base;
using Licenta.Core.Enums;

namespace Licenta.Core.Entities;

public class File : BaseEntity
{
    public string Name { get; set; }
    public string? Uri { get; set; }
    public FileEntityEnum Entity { get; set; }
    public long? EventId { get; set; }
    public Event Event { get; set; }
    public long? PartnerId { get; set; }
    public Partner Partner { get; set; }
    public long? StudentId { get; set; }
    public Student Student { get; set; }
}
