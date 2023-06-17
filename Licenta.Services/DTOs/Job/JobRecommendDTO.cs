using Licenta.Core.Enums;

namespace Licenta.Services.DTOs.Job;

public class JobRecommendDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public TypeJobEnum Type { get; set; }
    public string PartnerName { get; set; }
    public int MinExperience { get; set; }
    public int MaxExperience { get; set; }
    public string Salary { get; set; }
    public string Address { get; set; }
}
