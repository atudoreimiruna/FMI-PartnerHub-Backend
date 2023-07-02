using Licenta.Core.Entities;
using Licenta.Core.Enums;
using Licenta.Core.Interfaces;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using Licenta.Services.Interfaces.External;
using Licenta.Services.DTOs.Model;
using Microsoft.EntityFrameworkCore;
using Licenta.Services.Exceptions;
using static Licenta_External.MLModel1;

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

    public async Task LoadData()
    {
        // use the last model from db

        var model = await _modelRepository
            .AsQueryable()
            .Where(x => x.Type == ModelEnum.JobRecommendation)
            .OrderByDescending(x => x.CreatedAt)
            .FirstOrDefaultAsync();

        if (model is null)
        {
            throw new CustomNotFoundException("No model found");
        }

        // var trainingDataPath = @"C:\Users\atudo\Github\Licenta-Backend\Licenta\recommendation-ratings-train.csv";

        var stream = new MemoryStream(model.Content);
        var mlModel = MlContext.Model.Load(stream, out var _);
        predictionEngine = MlContext.Model.CreatePredictionEngine<JobRating, JobRatingPrediction>(mlModel);

        //IDataView trainingDataView = MlContext.Data.LoadFromTextFile<JobRating>(trainingDataPath, hasHeader: true, separatorChar: ',');

        //return (trainingDataView);
    }

    private static IEstimator<ITransformer> BuildAndTrainModel(MLContext mlContext)
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
         //ITransformer model = trainerEstimator.Fit(dataView);

        return trainerEstimator;
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
        if (predictionEngine is null)
        {
            throw new CustomBadRequestException("Recommendation Model Unavailable!");
        }

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
        var input = await _studentJobRepository.AsQueryable()
            .Where(x => x.CreatedAt.Date >= DateTime.Now.Date.AddDays(-7))
            .Include(x => x.Student)
            .Include(x => x.Job)
            .Select(x => new JobRating
            {
                StudentId = x.StudentId,
                JobId = x.JobId,
                Label = Convert.ToInt32(x.JobRating)
            }).ToListAsync();


        // MLContext mlContext = new MLContext();

        // IDataView trainingDataView = await LoadData();

        var pipeline = BuildAndTrainModel(MlContext);
        var trainingDataView = MlContext.Data.LoadFromEnumerable(input);
        // Split the data into training and test datasets (80% training, 20% test)
        //var dataSplit = MlContext.Data.TrainTestSplit(trainingDataView, testFraction: 0.2);

        //IDataView trainingData = dataSplit.TrainSet;
        //IDataView testData = dataSplit.TestSet;

        Console.WriteLine("=============== Training the model ===============");
        ITransformer model = pipeline.Fit(trainingDataView);

        EvaluateModel(MlContext, trainingDataView, model);

        // predictionEngine = mlContext.Model.CreatePredictionEngine<JobRating, JobRatingPrediction>(model);

        await SaveModelAsync(MlContext, trainingDataView.Schema, model);

        await LoadData();
    }
}

