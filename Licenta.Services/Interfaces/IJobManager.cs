﻿using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Job;
using Licenta.Services.QueryParameters;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IJobManager
{
    Task<JobViewDTO> AddAsync(JobPostDTO jobDto, string partnerId);
    Task<PagedList<JobViewDTO>> ListJobsAsync(JobParameters parameters);
    Task<PagedList<JobViewDTO>> GetJobsOfPartnerAsync(long partnerId);
    Task<JobViewDTO> GetJobProfileByIdAsync(long id);
    Task<JobViewDTO> UpdateAsync(JobPutDTO jobDto, string partnerId);
    Task<JobViewDTO> UpdateActivatedAsync(JobPutActivatedDTO jobDto, string partnerId);
    Task DeleteAsync(long id, string partnerId);
}

