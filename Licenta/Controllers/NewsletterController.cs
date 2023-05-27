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

}
