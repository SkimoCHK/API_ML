using Microsoft.ML.Data;

namespace API_EVILWORDS.Models
{
  public class CommentPrediction
  {
    [ColumnName("PredictedLabel")]
    public bool PredictedLabel { get; set; }

    public float Score { get; set; }

    public float Probability { get; set; }

    public bool isBadWord { get; set; }

    public string Comment { get; set; }
  }
}
