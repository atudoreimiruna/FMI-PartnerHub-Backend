using Licenta.Core.Entities.Base;
using Licenta.Core.Enums;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class Job : BaseEntity
{
    public string Title { get; set; }
    public string Salary { get; set; }
    public JobExperienceEnum Experience { get; set; }
    public TypeJobEnum Type { get; set; }
    public int MinExperience { get; set; }
    public int MaxExperience { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Criteria { get; set; }
    public bool Activated { get; set; }
    public string Skills { get; set; }
    public long? PartnerId { get; set; }
    public Partner Partner { get; set; }
    public virtual ICollection<StudentJob> StudentJobs { get; set; } = new List<StudentJob>();
}

