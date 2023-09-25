using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Job;
using Licenta.Services.DTOs.Student;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IStudentManager
{
    Task DeleteAsync(long id);
    Task<StudentViewDTO> UpdateAsync(StudentPutDTO studentDto);
    Task<StudentViewDTO> GetStudentProfileByIdAsync(long id);
    Task<StudentViewDTO> GetStudentProfileByEmailAsync(string email);
    Task<StudentViewDTO> UpdateJobAsync(StudentJobPutDTO studentDto);
    Task<StudentViewDTO> UpdatePartnerAsync(StudentPartnerPutDTO studentDto);
    Task DeleteStudentPartnerAsync(long studentId, long partnerId);
    Task DeleteStudentJobAsync(long studentId, long jobId);
    Task<List<JobRecommendDTO>> GetRecommendedJobs(string email);
    Task<StudentJobViewDTO> GetStudentJobAsync(long studentId, long jobId);
    Task<PagedList<StudentJobViewDTO>> GetStudentJobsAsync(string email);
    Task<PagedList<StudentViewDTO>> GetStudentPartnersAsync(long partnerId, string tokenId);
}
