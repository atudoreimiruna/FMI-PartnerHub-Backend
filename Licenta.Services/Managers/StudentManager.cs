using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Student;
using Licenta.Services.Exceptions;
using Licenta.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class StudentManager : IStudentManager
{
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<StudentJob> _studentJobRepository;
    private readonly IMapper _mapper;

    public StudentManager(
        IRepository<Student> studentRepository,
        IRepository<StudentJob> studentJobRepository,
        IMapper mapper)
    {
        _studentRepository = studentRepository;
        _studentJobRepository = studentJobRepository;
        _mapper = mapper;
    }

    public async Task<StudentViewDTO> GetStudentProfileByIdAsync(long id)
    {
        var student = await _studentRepository
            .AsQueryable()
            .Include(x => x.Files)
            .Include(x => x.StudentJobs)
            .ThenInclude(x => x.Job)
            .ThenInclude(x => x.Partner)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            throw new CustomNotFoundException("Student Not Found");
        }
        return _mapper.Map<StudentViewDTO>(student);
    }

    public async Task<StudentViewDTO> GetStudentProfileByEmailAsync(string email)
    {
        var student = await _studentRepository
            .AsQueryable()
            .Include(x => x.Files)
            .Include(x => x.StudentJobs)
            .ThenInclude(x => x.Job)
            .ThenInclude(x => x.Partner)
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            throw new CustomNotFoundException("Student Not Found");
        }
        return _mapper.Map<StudentViewDTO>(student);
    }

    public async Task<StudentViewDTO> UpdateAsync(StudentPutDTO studentDto)
    {
        var student = await _studentRepository
            .AsQueryable()
            .Include(x => x.Files)
            .Include(x => x.StudentJobs)
            .ThenInclude(x => x.Job)
            .ThenInclude(x => x.Partner)
            .Where(x => x.Id == studentDto.Id)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            throw new CustomNotFoundException("Student Not Found");
        }

        _mapper.Map(studentDto, student);
        await _studentRepository.UpdateAsync(student);

        return await GetStudentProfileByIdAsync(studentDto.Id);
    }

    public async Task<StudentViewDTO> UpdateJobAsync(StudentJobPutDTO studentDto)
    {
        var student = await _studentRepository
            .AsQueryable()
            .Include(x => x.Files)
            .Include(x => x.StudentJobs)
            .ThenInclude(x => x.Job)
            .ThenInclude(x => x.Partner)
            .Where(x => x.Id == studentDto.Id)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            throw new CustomNotFoundException("Student Not Found");
        }

        var studentJob = new StudentJob { StudentId = studentDto.Id, JobId = studentDto.JobId };
        await _studentJobRepository.AddAsync(studentJob);

        _mapper.Map(studentDto, student);
        await _studentRepository.UpdateAsync(student);

        return await GetStudentProfileByIdAsync(studentDto.Id);
    }

    public async Task DeleteStudentJobAsync(long studentId, long jobId)
    {
        var studentJob = await _studentJobRepository.AsQueryable().Where(x => x.JobId == jobId && x.StudentId == studentId).FirstOrDefaultAsync();
        if (studentJob == null)
        {
            throw new CustomNotFoundException("StudentJob Not Found");
        }
        await _studentJobRepository.RemoveAsync(studentJob);
    }

    public async Task DeleteAsync(long id)
    {
        var student = await _studentRepository.FindByIdAsync(id);
        if (student == null)
        {
            throw new CustomNotFoundException("Student Not Found");
        }
        await _studentRepository.RemoveAsync(student);
    }
}
