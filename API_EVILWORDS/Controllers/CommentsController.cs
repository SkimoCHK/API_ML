using API_EVILWORDS.Interfaces;
using API_EVILWORDS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_EVILWORDS.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CommentsController : ControllerBase
  {
    private readonly ICommentService _service;
    public CommentsController(ICommentService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetComments() => Ok(await _service.LoadData());
  }
}
