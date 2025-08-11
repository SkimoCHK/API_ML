using API_EVILWORDS.Models;
using System.Threading.Tasks;

namespace API_EVILWORDS.Interfaces
{
  public interface ICommentService
  {
    Task<IEnumerable<Comment>> LoadData();
  }
}
