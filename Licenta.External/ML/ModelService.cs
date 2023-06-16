using Licenta.Core.Entities;
using Licenta.Core.Enums;
using Licenta.Core.Interfaces;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Licenta.Services.Interfaces.External;
using Licenta.Services.DTOs.Model;

namespace Licenta.External.ML;

public class ModelService : IModelService
{
    private static readonly MLContext MlContext = new MLContext();
    private static PredictionEngine<JobRating, JobRatingPrediction> predictionEngine = null;

    private readonly IRepository<Model> _modelRepository;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<Job> _jobRepository;
    private readonly IRepository<StudentJob> _studentJobRepository;

    public ModelService(IRepository<Model> modelRepository, IRepository<Student> studentRepository, IRepository<Job> jobRepository, IRepository<StudentJob> studentJobRepository)
    {
        _modelRepository = modelRepository;
        _studentRepository = studentRepository;
        _jobRepository = jobRepository;
        _studentJobRepository = studentJobRepository;
    }

    public async Task<IDataView> LoadData()
    {
        // use the last model from db
        var trainingDataPath = @"C:\Users\atudo\Github\Licenta-Backend\Licenta\recommendation-ratings-train.csv";

        IDataView trainingDataView = MlContext.Data.LoadFromTextFile<JobRating>(trainingDataPath, hasHeader: true, separatorChar: ',');

        return (trainingDataView);
    }

    public async Task<ITransformer> BuildAndTrainModel(MLContext mlContext, IDataView dataView)
    {
        IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "JobId", inputColumnName: "JobId")
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "StudentId", inputColumnName: "StudentId"));

        var options = new MatrixFactorizationTrainer.Options
        {
            MatrixColumnIndexColumnName = "StudentId",
            MatrixRowIndexColumnName = "JobId",
            LabelColumnName = "Label",
            NumberOfIterations = 20,
            ApproximationRank = 100,
        };

        var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

        //Console.WriteLine("=============== Training the model ===============");
        ITransformer model = trainerEstimator.Fit(dataView);

        return model;
    }

    public void EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
    {
        Console.WriteLine("=============== Evaluating the model ===============");
        var prediction = model.Transform(testDataView);
        var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Label", scoreColumnName: "Score");
        Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
        Console.WriteLine("RSquared: " + metrics.RSquared.ToString());
    }

    public JobRatingPrediction UseModelForSinglePrediction(JobRating jobRating)
    {
        Console.WriteLine("=============== Making a prediction ===============");
        
        return predictionEngine.Predict(jobRating);
    }

    public async Task SaveModelAsync(MLContext mlContext, DataViewSchema trainingDataViewSchema, ITransformer model)
    {
        Console.WriteLine("=============== Saving the model to a file ===============");
        var stream = new MemoryStream();
        mlContext.Model.Save(model, trainingDataViewSchema, stream);
        var bytes = stream.ToArray();
        await _modelRepository.AddAsync(new Model
        {
            Type = ModelEnum.JobRecommendation,
            Content = bytes
        });
        //await LoadModelAsync();
    }

    public async Task RunModelAsync()
    {
        MLContext mlContext = new MLContext();

        IDataView trainingDataView = await LoadData();

        // Split the data into training and test datasets (80% training, 20% test)
        var dataSplit = mlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

        IDataView trainingData = dataSplit.TrainSet;
        IDataView testData = dataSplit.TestSet;

        Console.WriteLine("=============== Training the model ===============");
        ITransformer model = await BuildAndTrainModel(mlContext, trainingData);

        EvaluateModel(mlContext, testData, model);

        predictionEngine = mlContext.Model.CreatePredictionEngine<JobRating, JobRatingPrediction>(model);

        await SaveModelAsync(mlContext, trainingDataView.Schema, model);
    }
}


