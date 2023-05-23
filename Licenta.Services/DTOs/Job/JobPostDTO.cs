using Licenta.Core.Enums;

namespace Licenta.Services.DTOs.Job;

public class JobPostDTO
{
    public string Title { get; set; }
    public string Salary { get; set; }
    public int MinExperience { get; set; }
    public int MaxExperience { get; set; }
    public TypeJobEnum Type { get; set; }
    public JobExperienceEnum Experience { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string Criteria { get; set; }
    public string Skills { get; set; }
    public long? PartnerId { get; set; }
}
