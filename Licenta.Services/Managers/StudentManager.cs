using AutoMapper;
using Hangfire.Common;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Job;
using Licenta.Services.DTOs.Student;
using Licenta.Services.Exceptions;
using Licenta.Services.Interfaces;
using Licenta.Services.Interfaces.External;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class StudentManager : IStudentManager
{
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<StudentJob> _studentJobRepository;
    private readonly IRepository<StudentPartner> _studentPartnerRepository;
    private readonly IModelService _modelService;
    private readonly IMapper _mapper;

    public StudentManager(
        IRepository<Student> studentRepository,
        IRepository<StudentJob> studentJobRepository,
        IRepository<StudentPartner> studentPartnerRepository,
        IModelService modelService,
        IMapper mapper)
    {
        _studentRepository = studentRepository;
        _studentJobRepository = studentJobRepository;
        _studentPartnerRepository = studentPartnerRepository;
        _modelService = modelService;
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
            .Include(x => x.StudentPartners)
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

        var studentJob = await _studentJobRepository
            .AsQueryable()
            .Where(x => x.JobId == studentDto.JobId && x.StudentId == studentDto.Id)
            .FirstOrDefaultAsync();

        if (studentJob == null)
        {
            var newStudentJob = new StudentJob
            {
                StudentId = studentDto.Id,
                JobId = studentDto.JobId,
                JobStatus = studentDto.JobStatus,
                JobRating = studentDto.JobRating
            };
            await _studentJobRepository.AddAsync(newStudentJob);
        }
        else
        {
            _mapper.Map(studentDto, studentJob);
            await _studentRepository.UpdateAsync(student);
        }

        return await GetStudentProfileByIdAsync(studentDto.Id);
    }

    public async Task<StudentViewDTO> UpdatePartnerAsync(StudentPartnerPutDTO studentDto)
    {
        var student = await _studentRepository
            .AsQueryable()
            .Include(x => x.Files)
            .Include(x => x.StudentPartners)
            .ThenInclude(x => x.Partner)
            .Where(x => x.Id == studentDto.Id)
            .FirstOrDefaultAsync();

        if (student == null)
        {
            throw new CustomNotFoundException("Student Not Found");
        }

        var studentPartner = new StudentPartner { StudentId = studentDto.Id, PartnerId = studentDto.PartnerId };
        await _studentPartnerRepository.AddAsync(studentPartner);

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

    public async Task DeleteStudentPartnerAsync(long studentId, long partnerId)
    {
        var studentPartner = await _studentPartnerRepository.AsQueryable().Where(x => x.PartnerId == partnerId && x.StudentId == studentId).FirstOrDefaultAsync();
        if (studentPartner == null)
        {
            throw new CustomNotFoundException("StudentPartner Not Found");
        }
        await _studentPartnerRepository.RemoveAsync(studentPartner);
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

    public async Task<StudentJobViewDTO> GetStudentJobAsync(long studentId, long jobId)
    {
        var studentJob = await _studentJobRepository
            .AsQueryable()
            .Where(x => x.JobId == jobId && x.StudentId == studentId)
            .FirstOrDefaultAsync();

        //if (studentJob == null)
        //{
        //    throw new CustomNotFoundException("StudentJob Not Found");
        //}
        return _mapper.Map<StudentJobViewDTO>(studentJob);
    }

    public async Task<PagedList<StudentViewDTO>> GetStudentPartnersAsync(long partnerId, string tokenId)
    {
        if (tokenId != null && partnerId == long.Parse(tokenId))
        {
            var studentPartners = await _studentPartnerRepository
                .AsQueryable()
                .Include(x => x.Student)
                .ThenInclude(x => x.Files)
                .Where(x => x.PartnerId == partnerId)
                .ToListAsync();

            var students = new List<Student>();
            foreach(var studentPartner in studentPartners)
            {
                students.Add(studentPartner.Student);
            }
            return _mapper.Map<PagedList<StudentViewDTO>>(students);
        }
        else
        {
            throw new CustomNotFoundException("Students Not Found");
        }
    }

    public async Task<PagedList<StudentJobViewDTO>> GetStudentJobsAsync(string email)
    {
        var student = await _studentRepository
            .AsQueryable()
            .Include(x => x.StudentJobs)
            .ThenInclude(x => x.Job)
            .Where(x => x.Email == email)
            .FirstOrDefaultAsync();

        var studentJobs = await _studentJobRepository
            .AsQueryable()
            .Where(x => x.StudentId == student.Id)
            .ToListAsync();

        if (studentJobs == null)
        {
            throw new CustomNotFoundException("StudentJobs Not Found");
        }
        return _mapper.Map<PagedList<StudentJobViewDTO>>(studentJobs);
    }


    public async Task<List<JobRecommendDTO>> GetRecommendedJobs(string email)
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

        // joburile pe care studentul nu le a evaluat si au rating mai mare de 3
        var bestJobs = await _studentJobRepository
            .AsQueryable()
            .Where(x => x.StudentId != student.Id && (x.JobRating == StudentJobRatingEnum.VeryInterested || x.JobRating == StudentJobRatingEnum.SomewhatInterested))
            .Take(10)
            .ToListAsync();

        var recommendedJobs = bestJobs
            .DistinctBy(x => x.StudentId)
            .OrderByDescending(x => _modelService
            .UseModelForSinglePrediction(new DTOs.Model.JobRating
            {
                StudentId= student.Id,
                JobId = x.JobId
            }).Score)
            .Take(5)
            .ToList();

        foreach (var job in recommendedJobs) { Console.WriteLine("Job " + job.JobId + " is recommended for user " + email); }

        return bestJobs.Select(_mapper.Map<JobRecommendDTO>).ToList();
    }
}