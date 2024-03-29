﻿using Microsoft.AspNetCore.Mvc;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Student;
using Licenta.External.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace Licenta.Api.Controllers;

[Route("api/students")]
[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme}")]
public class StudentController : ControllerBase
{
    private readonly IStudentManager _studentManager;
    public StudentController(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> GetStudentProfile([FromRoute] long id)
    {
        var student = await _studentManager.GetStudentProfileByIdAsync(id);
        return Ok(student);
    }

    [HttpGet("by/{email}")]
    [Authorize(Roles = "User,Admin,SuperAdmin")]
    public async Task<IActionResult> GetStudent([FromRoute] string email)
    {
        var student = await _studentManager.GetStudentProfileByEmailAsync(email);
        return Ok(student);
    }

    [HttpPut]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> UpdateAsync([FromBody] StudentPutDTO studentDto)
    {
        var student = await _studentManager.UpdateAsync(studentDto);
        return Ok(student);
    }

    [HttpPut("job")]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> UpdateJobAsync([FromBody] StudentJobPutDTO studentDto)
    {
        var student = await _studentManager.UpdateJobAsync(studentDto);
        return Ok(student);
    }

    [HttpDelete("{studentId}/{jobId}")]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> DeleteStudentJob([FromRoute] long studentId, [FromRoute] long jobId)
    {
        await _studentManager.DeleteStudentJobAsync(studentId, jobId);
        return Ok("StudentJob DELETED successfully");
    }

    [HttpPut("partner")]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> UpdatePartnerAsync([FromBody] StudentPartnerPutDTO studentDto)
    {
        var student = await _studentManager.UpdatePartnerAsync(studentDto);
        return Ok(student);
    }

    [HttpDelete("by/{studentId}/{partnerId}")]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> DeleteStudentPartner([FromRoute] long studentId, [FromRoute] long partnerId)
    {
        await _studentManager.DeleteStudentPartnerAsync(studentId, partnerId);
        return Ok("StudentPartner DELETED successfully");
    }

    [HttpGet("by/{studentId}/{jobId}")]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> GetStudentJob([FromRoute] long studentId, [FromRoute] long jobId)
    {
        var studentJob = await _studentManager.GetStudentJobAsync(studentId, jobId);
        return Ok(studentJob);
    }

    [HttpGet("partnerStudents/{partnerId}")]
    [Authorize(AuthPolicy.Admin)]
    public async Task<IActionResult> GetStudentPartners([FromRoute] long partnerId)
    {
        var tokenId = User.FindFirstValue("partnerId");

        var studentPartners = await _studentManager.GetStudentPartnersAsync(partnerId, tokenId);
        return Ok(studentPartners);
    }

    // TODO: add endpoint for job applications 

    [HttpDelete("{id}")]
    [Authorize(AuthPolicy.SuperAdmin)]
    public async Task<IActionResult> DeleteStudent([FromRoute] long id)
    {
        await _studentManager.DeleteAsync(id);
        return Ok("Student DELETED successfully");
    }

    [HttpGet("jobs-for-student/{email}")]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> JobForStudent([FromRoute] string email)
    {
        var result = await _studentManager.GetRecommendedJobs(email);
        return Ok(result);
    }

    [HttpGet("jobs/{email}")]
    [Authorize(AuthPolicy.User)]
    public async Task<IActionResult> JobsForStudent([FromRoute] string email)
    {
        var result = await _studentManager.GetStudentJobsAsync(email);
        return Ok(result);
    }
}
