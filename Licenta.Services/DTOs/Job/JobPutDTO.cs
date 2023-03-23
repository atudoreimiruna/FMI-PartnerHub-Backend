using Licenta.Core.Enums;

namespace Licenta.Services.DTOs.Job;

public class JobPutDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public long? MinSalary { get; set; }
    public long? MaxSalary { get; set; }
    public JobExperienceEnum? Experience { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public long? PartnerId { get; set; }
}
