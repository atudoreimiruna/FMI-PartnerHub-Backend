using Licenta.Core.Entities.Base;
using Licenta.Core.Enums;

namespace Licenta.Core.Entities;

public class Model : BaseEntity
{
    public ModelEnum Type { get; set; }
    public byte[] Content { get; set; }
}
