using Licenta.Core.Entities.Base;
using System;

namespace Licenta.Services.DTOs.Base;

public class BaseDto : IBaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdated { get; set; }
}
