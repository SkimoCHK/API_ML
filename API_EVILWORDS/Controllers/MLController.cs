using API_EVILWORDS.DTOs;
using API_EVILWORDS.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;


namespace API_EVILWORDS.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MLController : ControllerBase
  {
    private readonly IMLService _mlService;
    public MLController(IMLService mlService) => _mlService = mlService;

    [HttpPost]
    public async Task<IActionResult> EvaluateText([FromBody] InputDto input)
    {
      var predictions = await _mlService.EvaluateText(input.Text);
      return Created(nameof(Created), predictions);
    }
  }
}
