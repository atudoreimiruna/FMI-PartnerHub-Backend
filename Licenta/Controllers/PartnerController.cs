﻿using Microsoft.AspNetCore.Mvc;
using Licenta.Services.Interfaces;
using Licenta.Services.QueryParameters.Partner;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Partner;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPartnerProfile([FromRoute] long id)
    {
        var partner = await _partnerManager.GetPartnerProfileByIdAsync(id);
        return Ok(partner);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] PartnerPutDTO partnerDto)
    {
        var partner = await _partnerManager.UpdateAsync(partnerDto);
        return Ok(partner);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePartner([FromRoute] long id)
    {
        await _partnerManager.DeleteAsync(id);
        return Ok("Partner DELETED successfully");
    }
}