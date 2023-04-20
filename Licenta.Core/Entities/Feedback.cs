using Licenta.Core.Entities.Base;

namespace Licenta.Core.Entities;

public class Feedback : BaseEntity
{
    public string Name { get; set; }
    public string Message { get; set; }
}
