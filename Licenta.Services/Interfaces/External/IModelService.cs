using Licenta.Services.DTOs.Model;
using Microsoft.ML;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces.External;

public interface IModelService
{
    Task<IDataView> LoadData();
    Task<ITransformer> BuildAndTrainModel(MLContext mlContext, IDataView dataView);
    void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model);
    Task RunModelAsync();
    Task SaveModelAsync(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model);
    JobRatingPrediction UseModelForSinglePrediction(JobRating jobRating);

}
