using Microsoft.AspNetCore.Mvc;
using Licenta.Infrastructure;

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

}
