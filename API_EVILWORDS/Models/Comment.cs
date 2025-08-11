using Microsoft.ML.Data;

namespace API_EVILWORDS.Models
{
  public class Comment
  {
    [NoColumn]
    public int? Id { get; set; }
    public string? Text { get; set; }
    public bool Label { get; set; }
  }
}
