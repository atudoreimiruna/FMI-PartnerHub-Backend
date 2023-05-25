using Microsoft.AspNetCore.Mvc;
using Licenta.Services.Interfaces;
using System.Threading.Tasks;
using Licenta.Services.DTOs.Student;

namespace Licenta.Api.Controllers;

[Route("api/students")]
[ApiController]
public class StudentController : ControllerBase
{
    private readonly IStudentManager _studentManager;
    public StudentController(IStudentManager studentManager)
    {
        _studentManager = studentManager;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentProfile([FromRoute] long id)
    {
        var student = await _studentManager.GetStudentProfileByIdAsync(id);
        return Ok(student);
    }

    [HttpGet("by/{email}")]
    public async Task<IActionResult> GetStudent([FromRoute] string email)
    {
        var student = await _studentManager.GetStudentProfileByEmailAsync(email);
        return Ok(student);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] StudentPutDTO studentDto)
    {
        var student = await _studentManager.UpdateAsync(studentDto);
        return Ok(student);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent([FromRoute] long id)
    {
        await _studentManager.DeleteAsync(id);
        return Ok("Student DELETED successfully");
    }
}
