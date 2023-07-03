using Licenta.Core.Enums;
using Licenta.Services.DTOs.Base;
using Licenta.Services.DTOs.Student;
using System.Collections.Generic;

namespace Licenta.Services.DTOs.Job;

public class JobViewDTO : BaseDto
{
    public string Title { get; set; }
    public string Salary { get; set; }
    public int MinExperience { get; set; }
    public int MaxExperience { get; set; }
    public JobExperienceEnum Experience { get; set; }
    public TypeJobEnum Type { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Criteria { get; set; }
    public string Skills { get; set; }
    public bool Activated { get; set; }
    public string PartnerLogo { get; set; }
    public string PartnerName { get; set; }
    public long PartnerId {  get; set; }
    public List<StudentJobDetailsDTO> JobStudents { get; set; }
}
