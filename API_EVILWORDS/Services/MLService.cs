using API_EVILWORDS.Interfaces;
using API_EVILWORDS.Models;
using Microsoft.ML;
using Microsoft.ML.Calibrators;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;

namespace API_EVILWORDS.Services
{
  public class MLService : IMLService
  {
    private readonly MLContext _mlContext;
    private readonly PredictionEngine<Comment, CommentPrediction> _predictor;
    private const float PROBABILITY_THRESHOLD = 0.85f;

    public MLService(IServiceScopeFactory scopeFactory)
    {
      _mlContext = new MLContext();

      using var scope = scopeFactory.CreateScope();
      var commentService = scope.ServiceProvider.GetRequiredService<ICommentService>();

      var comments = commentService.LoadData().Result;
      var trainingData = _mlContext.Data.LoadFromEnumerable<Comment>(comments);

      var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(Comment.Text))
          .Append(_mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(
              labelColumnName: "Label",
              featureColumnName: "Features"
          ));

      var model = pipeline.Fit(trainingData);

      _predictor = _mlContext.Model.CreatePredictionEngine<Comment, CommentPrediction>(model);
    }

    public Task<IEnumerable<CommentPrediction>> EvaluateText(string text)
    {
      var prediction = _predictor.Predict(new Comment { Text = text });
      prediction.isBadWord = prediction.Probability > PROBABILITY_THRESHOLD;
      prediction.Comment = text;

      return Task.FromResult<IEnumerable<CommentPrediction>>(new[] { prediction });
    }
  }
}
