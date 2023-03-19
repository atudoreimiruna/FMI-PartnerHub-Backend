using Microsoft.AspNetCore.Mvc;
using Licenta.Core.Entities;
using Licenta.Infrastructure;
using Licenta.Services.DTOs;
using System.Threading.Tasks;

namespace Licenta.Api.Controllers;

[Route("api/feedback")]
[ApiController]
public class FeedbackController : ControllerBase
{
    private readonly AppDbContext _context;

    public FeedbackController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost("fromBody")]
    public async Task<IActionResult> Create(FeedbackPostModel model)
    {
        if (string.IsNullOrEmpty(model.Name))
        {
            return BadRequest("Invalid object. Model is null");
        }

        var feedback = new Feedback()
        {
            Name = model.Name,
            Message = model.Message
        };

        await _context.Feedbacks.AddRangeAsync(feedback);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
