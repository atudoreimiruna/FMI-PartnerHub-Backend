﻿using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.DTOs.Event;
using Licenta.Services.DTOs.Job;
using Licenta.Services.DTOs.Partner;
using Licenta.Services.DTOs.Practice;
using Licenta.Services.DTOs.Student;
using System.Collections.Generic;
using System.Linq;

namespace Licenta.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserViewDTO>()
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role.Name).ToList()))
            .ReverseMap();

        CreateMap<Practice, PracticeViewDTO>().ReverseMap();
        CreateMap<Practice, PracticePutDTO>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Student, StudentPostDTO>().ReverseMap();
        CreateMap<Student, StudentViewDTO>()
            .ForMember(dest => dest.FileNames, opt => opt.MapFrom(src => src.Files.Select(x => x.Name).ToList()))
            .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.StudentJobs.Select(x => x.Job).ToList()))
            .ForMember(dest => dest.Partners, opt => opt.MapFrom(src => src.StudentPartners.Select(x => x.Partner).ToList()))
            .ReverseMap();
        CreateMap<Student, StudentPutDTO>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<StudentJob, StudentJobPutDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.StudentId))
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
            .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.JobStatus))
            .ForMember(dest => dest.JobRating, opt => opt.MapFrom(src => src.JobRating))
            .ReverseMap();
        CreateMap<StudentJob, StudentJobViewDTO>()
           .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
           .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
           .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.JobStatus))
           .ForMember(dest => dest.JobRating, opt => opt.MapFrom(src => src.JobRating))
           .ReverseMap();

        CreateMap<StudentPartner, StudentPartnerViewDTO>()
           .ForMember(dest => dest.PartnerId, opt => opt.MapFrom(src => src.PartnerId))
           .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student))
           .ReverseMap();

        CreateMap<StudentJob, StudentJobDetailsDTO>()
          .ForMember(dest => dest.Student, opt => opt.MapFrom(src => src.Student))
          .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.JobId))
          .ForMember(dest => dest.JobStatus, opt => opt.MapFrom(src => src.JobStatus))
          .ForMember(dest => dest.JobRating, opt => opt.MapFrom(src => src.JobRating))
          .ReverseMap();

        CreateMap<Student, StudentPartnerPutDTO>()
           .ForMember(dest => dest.PartnerId, opt => opt.MapFrom(src => src.StudentPartners.Select(x => x.PartnerId).FirstOrDefault()))
           .ReverseMap();

        CreateMap<Partner, PartnerPostDTO>().ReverseMap();
        CreateMap<Partner, PartnerViewDTO>()
            .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.Jobs))
            .ReverseMap();
        CreateMap<Partner, PartnerPutDTO>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Event, EventPostDTO>().ReverseMap();
        CreateMap<Event, EventViewDTO>()
            .ForMember(dest => dest.Files, opt => opt.MapFrom(src => src.Files))
            .ForMember(dest => dest.PartnerName, opt => opt.MapFrom(src => src.Partner.Name))
            .ReverseMap();
        CreateMap<Event, EventPutDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Job, JobPostDTO>().ReverseMap();
        CreateMap<Job, JobViewDTO>()
            .ForMember(dest => dest.PartnerLogo, opt => opt.MapFrom(src => src.Partner.LogoImageUrl))
            .ForMember(dest => dest.PartnerName, opt => opt.MapFrom(src => src.Partner.Name))
            .ForMember(dest => dest.PartnerId, opt => opt.MapFrom(src => src.Partner.Id))
            .ForMember(dest => dest.JobStudents, opt => opt.MapFrom(src => src.StudentJobs))
            .ReverseMap();

        CreateMap<Job, JobPutActivatedDTO>().ReverseMap();
        CreateMap<Job, JobPutDTO>() 
            .ReverseMap()
            .ForMember(opt => opt.Experience, src => src.Ignore())
            .ForMember(opt => opt.Type, src => src.Ignore())
            .ForMember(opt => opt.PartnerId, src => src.Ignore())
            .ForMember(opt => opt.MinExperience, src => src.Ignore())
            .ForMember(opt => opt.MaxExperience, src => src.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<StudentJob, JobRecommendDTO>()
             .ForMember(opt => opt.Id, opt => opt.MapFrom(src => src.JobId))
             .ForMember(opt => opt.Title, opt => opt.MapFrom(src => src.Job.Title))
             .ForMember(opt => opt.Type, opt => opt.MapFrom(src => src.Job.Type))
             .ForMember(opt => opt.PartnerName, opt => opt.MapFrom(src => src.Job.Partner.Name))
             .ForMember(opt => opt.Address, opt => opt.MapFrom(src => src.Job.Address))
             .ForMember(opt => opt.Salary, opt => opt.MapFrom(src => src.Job.Salary))
             .ForMember(opt => opt.MinExperience, opt => opt.MapFrom(src => src.Job.MinExperience))
             .ForMember(opt => opt.MaxExperience, opt => opt.MapFrom(src => src.Job.MaxExperience))
             .ReverseMap();

        /// <summary>
        /// Mappings between PagedList and PagedList.
        /// </summary>
        CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));

        /// <summary>
        /// Mappings between List and PagedList.
        /// </summary>
        CreateMap(typeof(List<>), typeof(PagedList<>))
            .ConvertUsing(typeof(ListToPagedListConverter<,>));
    }

    // CONVERTERS
    public class PagedListConverter<TSource, TDestination> : ITypeConverter<PagedList<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(PagedList<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var items = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.AsEnumerable());

            return new PagedList<TDestination>(items, source.TotalCount, source.CurrentPage, source.PageSize, source.IsEnabled);
        }
    }

    public class ListToPagedListConverter<TSource, TDestination> : ITypeConverter<List<TSource>, PagedList<TDestination>> where TSource : class where TDestination : class
    {
        public PagedList<TDestination> Convert(List<TSource> source, PagedList<TDestination> destination, ResolutionContext context)
        {
            var items = context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.AsEnumerable());

            return new PagedList<TDestination>(items, source.Count, 1, source.Count, false);
        }
    }

}
