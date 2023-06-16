using CsvHelper;
using Licenta.Services.DTOs.CSV;
using System.Globalization;

namespace Licenta.External.CSV;

public class CSVService : ICSVService
{
    public async Task CreateCSV(List<RecommendationRatingTrainDTO> ratingDto)
    {
        using (var writer = new StreamWriter("recommendation-ratings-train.csv"))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(ratingDto);
        }
    }
}
