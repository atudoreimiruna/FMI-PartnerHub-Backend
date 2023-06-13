using Microsoft.AspNetCore.Mvc;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Partner;
using Licenta.Services.QueryParameters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Licenta.External.Authorization;
using System.Security.Claims;

namespace Licenta.Api.Controllers;

[Route("api/partners")]
[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme}")]
public class PartnerController : ControllerBase
{
    private readonly IPartnerManager _partnerManager;
    public PartnerController(IPartnerManager partnerManager)
    {
        _partnerManager = partnerManager;
    }

    [HttpPost]
    [Authorize(AuthPolicy.SuperAdmin)]
    public async Task<IActionResult> AddPartner([FromBody] PartnerPostDTO partnerDto)
    {
        var partner = await _partnerManager.AddAsync(partnerDto);
        return Ok(partner);
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> ListPartners([FromQuery] PartnerParameters parameters)
    {
        var partners = await _partnerManager.ListPartnersAsync(parameters);
        return Ok(partners);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> GetPartnerProfile([FromRoute] long id)
    {
        var partner = await _partnerManager.GetPartnerProfileByIdAsync(id);
        return Ok(partner);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync([FromBody] PartnerPutDTO partnerDto)
    {
        var partnerId = User.FindFirstValue("partnerId");
        var partner = await _partnerManager.UpdateAsync(partnerDto, partnerId);
        return Ok(partner);
    }

    [HttpDelete("{id}")]
    [Authorize(AuthPolicy.SuperAdmin)]
    public async Task<IActionResult> DeletePartner([FromRoute] long id)
    {
        await _partnerManager.DeleteAsync(id);
        return Ok("Partner DELETED successfully");
    }
}
