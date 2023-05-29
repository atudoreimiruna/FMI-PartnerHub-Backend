using Licenta.Services.DTOs.Student;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IStudentManager
{
    Task DeleteAsync(long id);
    Task<StudentViewDTO> UpdateAsync(StudentPutDTO studentDto);
    Task<StudentViewDTO> GetStudentProfileByIdAsync(long id);
    Task<StudentViewDTO> GetStudentProfileByEmailAsync(string email);
    Task<StudentViewDTO> UpdateJobAsync(StudentJobPutDTO studentDto);
    Task DeleteStudentJobAsync(long studentId, long jobId);
}
