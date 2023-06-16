using Microsoft.AspNetCore.Mvc;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using System.Security.Claims;
using Licenta.External.Authorization;
using Licenta.Services.QueryParameters;
using Licenta.External.SendGrid;
using System.Collections.Generic;
using Licenta.Services.DTOs.CSV;
using Licenta.External.CSV;
using Licenta.External.ML;
using Licenta.Services.Interfaces.External;

namespace Licenta.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _authManager;
    private readonly ISendgridManager _sendGridManager;
    private readonly ICSVService _csvService;
    private readonly IModelService _modelService;

    public AuthController(IAuthManager authManager, ISendgridManager sendGridManager, ICSVService csvService, IModelService modelService)
    {
        _authManager = authManager;
        _sendGridManager = sendGridManager;
        _csvService = csvService;
        _modelService = modelService;
    }

    [HttpPost("Login")]
    [Authorize(AuthenticationSchemes = $"{MicrosoftAccountDefaults.AuthenticationScheme},{Constants.AzureAd}")]
    public async Task<IActionResult> ExternalSignIn()
    {
        var loginModel = new LoginModel
        {
            Email = User.FindFirstValue("preferred_username"),
            Name = User.FindFirstValue("name"),
        };

        var result = await _authManager.Login(loginModel);

        if (result.Success == true)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Failed to login!");
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public async Task<IActionResult> ListUsers([FromQuery] UserParameters parameters)
    {
        var users = await _authManager.ListUsersAsync(parameters);
        return Ok(users);
    }


    [HttpPost("Role")]
    [Authorize(AuthPolicy.SuperAdmin)]
    public async Task<IActionResult> AddRoleToUser([FromBody] RegisterModel registerModel)
    {
        var result = await _authManager.AddRoleToUserAsync(registerModel);

        if (result)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Failed to add role!");
        }
    }

    [HttpPost("Partner")]
    [Authorize(AuthPolicy.SuperAdmin)]
    public async Task<IActionResult> AddPartnerToAdmin([FromBody] AdminPartnerDTO adminPartnerDTO)
    {
        var result = await _authManager.AddPartnerToAdminAsync(adminPartnerDTO);

        if (result)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Failed to add role!");
        }
    }

    [HttpDelete("Role")]
    [Authorize(AuthPolicy.SuperAdmin)]
    public async Task<IActionResult> RemoveRoleFromUser([FromBody] RegisterModel registerModel)
    {
        var result = await _authManager.RemoveRoleFromUserAsync(registerModel);

        if (result)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest("Failed to remove role!");
        }
    }

    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshModel refreshModel)
    {
        var result = await _authManager.Refresh(refreshModel);
        return !result.Contains("Bad") ? Ok(result) : BadRequest("Failed to refresh!");
    }

    [HttpPost("email")]
    public async Task<IActionResult> SendEmail([FromBody] List<SendgridUser> emailDtos)
    {
        var result = _sendGridManager.SendEmailTemplate(emailDtos);
        return Ok(result);
    }

    [HttpPost("csv")]
    public async Task<IActionResult> PostCSV([FromBody] List<RecommendationRatingTrainDTO> ratingDtos)
    {
        var result = _csvService.CreateCSV(ratingDtos);
        return Ok(result);
    }

    [HttpPost("model_training")]
    public async Task<IActionResult> TrainingModel()
    {
        await _modelService.RunModelAsync();
        return Ok();
    }
}