using Microsoft.AspNetCore.Mvc;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Job;
using Licenta.Services.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Licenta.Api.Controllers;

[Route("api/jobs")]
[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme}")]
public class JobController : ControllerBase
{
    private readonly IJobManager _jobManager;
    public JobController(IJobManager jobManager)
    {
        _jobManager = jobManager;
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddJob([FromBody] JobPostDTO jobDto)
    {
        var partnerId = User.FindFirstValue("partnerId");
        var job = await _jobManager.AddAsync(jobDto, partnerId);
        return Ok(job);
    }

    [HttpGet]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> ListJobs([FromQuery] JobParameters parameters)
    {
        var jobs = await _jobManager.ListJobsAsync(parameters);
        return Ok(jobs);
    }

    [HttpGet("{partnerId}")]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> GetJobsOfPartner([FromRoute] long partnerId)
    {
        var jobs = await _jobManager.GetJobsOfPartnerAsync(partnerId);
        return Ok(jobs);
    }

    [HttpGet("by/{id}")]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> GetJobProfile([FromRoute] long id)
    {
        var job = await _jobManager.GetJobProfileByIdAsync(id);
        return Ok(job);
    }

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateAsync([FromBody] JobPutDTO jobDto)
    {
        var partnerId = User.FindFirstValue("partnerId");
        var job = await _jobManager.UpdateAsync(jobDto, partnerId);
        return Ok(job);
    }

    [HttpPut("activated")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateActivatedAsync([FromBody] JobPutActivatedDTO jobDto)
    {
        var partnerId = User.FindFirstValue("partnerId");
        var job = await _jobManager.UpdateActivatedAsync(jobDto, partnerId);
        return Ok(job);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteJob([FromRoute] long id)
    {
        var partnerId = User.FindFirstValue("partnerId");
        await _jobManager.DeleteAsync(id, partnerId);
        return Ok("Job DELETED successfully");
    }
}
