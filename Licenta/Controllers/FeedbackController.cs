using Microsoft.AspNetCore.Mvc;
using Licenta.Core.Entities;
using Licenta.Infrastructure;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Feedback;

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
