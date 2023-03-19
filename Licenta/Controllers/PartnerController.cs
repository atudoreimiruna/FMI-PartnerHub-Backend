using Microsoft.AspNetCore.Mvc;
using Licenta.Services.Interfaces;
using Licenta.Services.QueryParameters.Partner;
using System.Threading.Tasks;

namespace Licenta.Api.Controllers;

[Route("api/partners")]
[ApiController]
public class PartnerController : ControllerBase
{
    private readonly IPartnerManager _partnerManager;
    public PartnerController(IPartnerManager partnerManager)
    {
        _partnerManager = partnerManager;
    }

    [HttpGet("partners")]
    public async Task<IActionResult> ListPartners([FromQuery] PartnerParameters parameters)
    {
        var partners = await _partnerManager.ListPartnersAsync(parameters);
        return Ok(partners);
    }
}
