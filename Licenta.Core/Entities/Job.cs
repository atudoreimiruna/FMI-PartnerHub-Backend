﻿using Licenta.Core.Entities.Base;
using Licenta.Core.Enums;

namespace Licenta.Core.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; }
    public long MinSalary { get; set; }
    public long MaxSalary { get; set; }
    public JobExperienceEnum Experience { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public bool Activated { get; set; }
    // TODO: Aptitudini (new entity)
    public long? PartnerId { get; set; }
    public Partner Partner { get; set; }
}

