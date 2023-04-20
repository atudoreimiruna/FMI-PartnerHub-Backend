using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Licenta.Core.Entities.Base;

public class BaseEntity : IBaseEntity
{
    public long Id { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedAt { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastUpdated { get; set; }
}
