using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Auth;
using Licenta.Services.DTOs.Event;
using Licenta.Services.DTOs.Job;
using Licenta.Services.DTOs.Partner;
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

        CreateMap<Student, StudentPostDTO>().ReverseMap();
        CreateMap<Student, StudentViewDTO>()
            .ForMember(dest => dest.FileNames, opt => opt.MapFrom(src => src.Files.Select(x => x.Name).ToList()))
            .ForMember(dest => dest.Jobs, opt => opt.MapFrom(src => src.StudentJobs.Select(x => x.Job).ToList()))
            .ReverseMap();
        CreateMap<Student, StudentPutDTO>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<Student, StudentJobPutDTO>()
            .ForMember(dest => dest.JobId, opt => opt.MapFrom(src => src.StudentJobs.Select(x => x.JobId).FirstOrDefault()))
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
            .ReverseMap();
        CreateMap<Event, EventPutDTO>()
            .ReverseMap()
            .ForMember(dest => dest.Date, opt => opt.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Job, JobPostDTO>().ReverseMap();
        CreateMap<Job, JobViewDTO>()
            .ForMember(dest => dest.PartnerLogo, opt => opt.MapFrom(src => src.Partner.LogoImageUrl))
            .ForMember(dest => dest.PartnerName, opt => opt.MapFrom(src => src.Partner.Name))
            .ReverseMap();

        CreateMap<Job, JobPutActivatedDTO>().ReverseMap();
        CreateMap<Job, JobPutDTO>() 
            .ReverseMap()
            .ForMember(opt => opt.Experience, src => src.Ignore())
            .ForMember(opt => opt.Type, src => src.Ignore())
            .ForMember(opt => opt.PartnerId, src => src.Ignore())
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

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
