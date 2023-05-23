using Licenta.Core.Enums;

namespace Licenta.Services.QueryParameters;

public class JobParameters : BaseParameters
{
    public string Title { get; set; }
    public string Address { get; set; }
    public JobExperienceEnum? Experience { get; set; }
    public int? MinExperience { get; set; }
    public int? MaxExperience { get; set; }
    public string PartnerName { get; set; }
}
