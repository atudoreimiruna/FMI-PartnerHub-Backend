using Licenta.Core.Enums;

namespace Licenta.Services.DTOs.Job;

public class JobRecommendDTO
{
    public long Id { get; set; }
    public string Title { get; set; }
    public TypeJobEnum Type { get; set; }
    public string LogoImageUrl { get; set; }
}
