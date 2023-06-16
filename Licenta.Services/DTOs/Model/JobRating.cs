using Microsoft.ML.Data;

namespace Licenta.Services.DTOs.Model;

public class JobRating
{
    [LoadColumn(0)]
    public long StudentId;
    [LoadColumn(1)]
    public long JobId;
    [LoadColumn(2)]
    public float Label;
}
