using System;

namespace Licenta.Core.Entities.Base;

public interface IBaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
}
