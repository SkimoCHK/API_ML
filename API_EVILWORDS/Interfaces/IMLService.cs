using API_EVILWORDS.Models;

namespace API_EVILWORDS.Interfaces
{
  public interface IMLService
  {
    Task<IEnumerable<CommentPrediction>> EvaluateText(string text);
  }
}
