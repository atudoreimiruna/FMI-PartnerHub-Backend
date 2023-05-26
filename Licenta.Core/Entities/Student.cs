using Licenta.Core.Entities.Base;
using System.Collections.Generic;

namespace Licenta.Core.Entities;

public class Student : BaseEntity
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string PersonalEmail { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Degree { get; set; }
    public string Skill { get; set; }
    public string Description { get; set; }
    public virtual ICollection<File> Files { get; set; } = new List<File>();
    public virtual ICollection<StudentJob> StudentJobs { get; set; } = new List<StudentJob>();
}
