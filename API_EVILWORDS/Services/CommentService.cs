using API_EVILWORDS.Interfaces;
using API_EVILWORDS.Models;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace API_EVILWORDS.Services
{
  public class CommentService : ICommentService
  {
    public async Task<IEnumerable<Comment>> LoadData()
    {
      Batteries_V2.Init(); 
      string db_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "comments.db");

      string query = "SELECT * FROM comments";
      List<Comment> comments = [];
      using (var cn = new SqliteConnection($"Data Source={db_path}"))
      {
        await cn.OpenAsync();
        var command = new SqliteCommand(query, cn);
        var rd = command.ExecuteReader();
        while (rd.Read())
        {
          comments.Add(new Comment()
          {
            Id = rd.GetInt32(0),
            Text = rd["Text"] as string,
            Label = rd.GetInt32(2) == 1
          });
        }
      }
      return comments;
    }
  }
}
