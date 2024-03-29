﻿using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Job;
using Licenta.Services.Exceptions;
using Licenta.Services.Interfaces;
using Licenta.Services.QueryParameters;
using Licenta.Services.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class JobManager : IJobManager
{
    private readonly IRepository<Job> _jobRepository;
    private readonly IMapper _mapper;

    public JobManager(
        IRepository<Job> jobRepository,
        IMapper mapper)
    {
        _jobRepository = jobRepository;
        _mapper = mapper;
    }
    public async Task<JobViewDTO> AddAsync(JobPostDTO jobDto, string partnerId)
    {
        if (partnerId != null && jobDto.PartnerId == long.Parse(partnerId))
        {
            var job = _mapper.Map<Job>(jobDto);

            await _jobRepository.AddAsync(job);

            return _mapper.Map<JobViewDTO>(job);
        }
        else
        {
            throw new CustomNotFoundException("The job cannot be added");
        }
    }

    public async Task<PagedList<JobViewDTO>> ListJobsAsync(JobParameters parameters)
    {
        var spec = new JobSpecification(parameters);
        var jobs = await _jobRepository.FindBySpecAsync(spec);
        return _mapper.Map<PagedList<JobViewDTO>>(jobs);
    }

    public async Task<PagedList<JobViewDTO>> GetJobsOfPartnerAsync(long partnerId)
    {
        var jobs = await _jobRepository
            .AsQueryable()
            .Include(x => x.StudentJobs)
            .ThenInclude(x => x.Student)
            .Include(x => x.Partner)
            .Where(x => x.PartnerId == partnerId)
            .ToListAsync();

        return _mapper.Map<PagedList<JobViewDTO>>(jobs);
    }

    public async Task<JobViewDTO> GetJobProfileByIdAsync(long id)
    {
        var job = await _jobRepository
            .AsQueryable()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (job == null)
        {
            throw new CustomNotFoundException("Job Not Found");
        }
        return _mapper.Map<JobViewDTO>(job);
    }

    public async Task<JobViewDTO> UpdateAsync(JobPutDTO jobDto, string partnerId)
    {
        var result = await _jobRepository
            .AsQueryable()
            .Include(x => x.Partner)
            .Where(x => x.Id == jobDto.Id)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new CustomNotFoundException("Job Not Found");
        }

        if (partnerId != null && result.PartnerId == long.Parse(partnerId))
        {
            _mapper.Map(jobDto, result);
            if (jobDto.Experience != null) { result.Experience = jobDto.Experience.Value; }
            if (jobDto.Type != null) { result.Type = jobDto.Type.Value; }
            if (jobDto.PartnerId != 0) { result.PartnerId = jobDto.PartnerId; }
            if (jobDto.MinExperience != 0) { result.MinExperience = jobDto.MinExperience; }
            if (jobDto.MaxExperience != 0) { result.MaxExperience = jobDto.MaxExperience; }

            await _jobRepository.UpdateAsync(result);

            return await GetJobProfileByIdAsync(jobDto.Id);
        }
        else
        {
            throw new CustomNotFoundException("The job cannot be updated");
        }
    }

    public async Task<JobViewDTO> UpdateActivatedAsync(JobPutActivatedDTO jobDto, string partnerId)
    {
        if (partnerId != null && jobDto.Id == long.Parse(partnerId))
        {
            var job = await _jobRepository.FindByIdAsync(jobDto.Id);

            if (job == null)
            {
                throw new CustomNotFoundException("Job Not Found");
            }

            _mapper.Map(jobDto, job);

            await _jobRepository.UpdateAsync(job);

            return await GetJobProfileByIdAsync(jobDto.Id);
        }
        else
        {
            throw new CustomNotFoundException("The job cannot be updated");
        }
    }

    public async Task DeleteAsync(long id, string partnerId)
    {
        var job = await _jobRepository.FindByIdAsync(id);
        if (job == null)
        {
            throw new CustomNotFoundException("Job Not Found");
        }
        if (partnerId != null && job.PartnerId == long.Parse(partnerId))
        {
            await _jobRepository.RemoveAsync(job);
        }
        else
        {
            throw new CustomNotFoundException("The job cannot be deleted");
        }
    }
}
