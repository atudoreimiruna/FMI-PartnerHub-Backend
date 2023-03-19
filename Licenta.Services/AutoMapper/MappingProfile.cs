using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Partner;
using System.Collections.Generic;
using System.Linq;

namespace Licenta.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Partner, PartnerViewDTO>().ReverseMap();
        CreateMap<Partner, PartnerPutDTO>()
            .ReverseMap()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        /// <summary>
        /// Mappings between PagedList and PagedList.
        /// </summary>
        CreateMap(typeof(PagedList<>), typeof(PagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
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

}
