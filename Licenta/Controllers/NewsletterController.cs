using Microsoft.AspNetCore.Mvc;
using Licenta.Core.Entities;
using Licenta.Infrastructure;
using Licenta.Services.DTOs;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Newsletter;

namespace Licenta.Api.Controllers;

[Route("api/newsletter")]
[ApiController]
public class NewsletterController : ControllerBase
{

    private readonly AppDbContext _context;

    public NewsletterController(AppDbContext context)
    {
        _context = context;
    }
    

    [HttpPost("fromBody")]
    public async Task<IActionResult> CreateNewsLetter(NewsletterPostDTO model)
    {
        if (string.IsNullOrEmpty(model.Email))
        {
            return BadRequest("Invalid object. Model is null");
        }

        var abonat = new Newsletter()
        {
            Email = model.Email
        };

        await _context.Newsletters.AddRangeAsync(abonat);
        await _context.SaveChangesAsync();

        return Ok();
    }
}
