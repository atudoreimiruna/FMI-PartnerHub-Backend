using Microsoft.AspNetCore.Mvc;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Job;
using Licenta.Services.QueryParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Licenta.Api.Controllers;

[Route("api/jobs")]
[ApiController]
//[Authorize]
public class JobController : ControllerBase
{
    private readonly IJobManager _jobManager;
    public JobController(IJobManager jobManager)
    {
        _jobManager = jobManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddJob([FromBody] JobPostDTO jobDto)
    {
        var job = await _jobManager.AddAsync(jobDto);
        return Ok(job);
    }

    [HttpGet]
    public async Task<IActionResult> ListJobs([FromQuery] JobParameters parameters)
    {
        var jobs = await _jobManager.ListJobsAsync(parameters);
        return Ok(jobs);
    }

    [HttpGet("{partnerId}")]
    public async Task<IActionResult> GetJobsOfPartner([FromRoute] long partnerId)
    {
        var jobs = await _jobManager.GetJobsOfPartnerAsync(partnerId);
        return Ok(jobs);
    }

    [HttpGet("by/{id}")]
    public async Task<IActionResult> GetJobProfile([FromRoute] long id)
    {
        var job = await _jobManager.GetJobProfileByIdAsync(id);
        return Ok(job);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] JobPutDTO jobDto)
    {
        var job = await _jobManager.UpdateAsync(jobDto);
        return Ok(job);
    }

    [HttpPut("activated")]
    public async Task<IActionResult> UpdateActivatedAsync([FromBody] JobPutActivatedDTO jobDto)
    {
        var job = await _jobManager.UpdateActivatedAsync(jobDto);
        return Ok(job);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteJob([FromRoute] long id)
    {
        await _jobManager.DeleteAsync(id);
        return Ok("Job DELETED successfully");
    }
}
