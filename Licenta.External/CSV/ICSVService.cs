using Licenta.Services.DTOs.CSV;

namespace Licenta.External.CSV;

public interface ICSVService
{
    Task CreateCSV(List<RecommendationRatingTrainDTO> ratingDto);
}
