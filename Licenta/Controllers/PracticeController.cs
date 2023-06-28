using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Licenta.Services.Interfaces;
using Licenta.Services.DTOs.Practice;

namespace Licenta.Api.Controllers;

[Route("api/practice")]
[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme}")]
public class PracticeController : ControllerBase
{
    private readonly IPracticeManager _practiceManager;
    public PracticeController(IPracticeManager practiceManager)
    {
        _practiceManager = practiceManager;
    }

    [HttpGet("by/{id}")]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> GetPractice([FromRoute] long id)
    {
        var practice = await _practiceManager.GetPracticeByIdAsync(id);
        return Ok(practice);
    }

    [HttpPut]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UpdateAsync([FromBody] PracticePutDTO practiceDto)
    {
        var practice = await _practiceManager.UpdateAsync(practiceDto);
        return Ok(practice);
    }
}
