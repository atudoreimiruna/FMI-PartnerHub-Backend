using Licenta.Core.Enums;

namespace Licenta.Services.QueryParameters;

public class JobParameters : BaseParameters
{
    public string Title { get; set; }
    public string Address { get; set; }
    public JobExperienceEnum? Experience { get; set; }
    public long? MinSalary { get; set; }
    public long? MaxSalary { get; set; }
}
