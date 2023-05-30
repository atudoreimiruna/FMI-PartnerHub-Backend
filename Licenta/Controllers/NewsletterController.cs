using Microsoft.AspNetCore.Mvc;
using Licenta.Infrastructure;

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

}
