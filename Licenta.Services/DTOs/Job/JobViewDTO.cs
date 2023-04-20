using Licenta.Core.Enums;
using Licenta.Services.DTOs.Base;

namespace Licenta.Services.DTOs.Job;

public class JobViewDTO : BaseDto
{
    public string Title { get; set; }
    public long MinSalary { get; set; }
    public long MaxSalary { get; set; }
    public JobExperienceEnum Experience { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public bool Activated { get; set; }
    public string PartnerLogo { get; set; }
    public string PartnerName { get; set; }
}
