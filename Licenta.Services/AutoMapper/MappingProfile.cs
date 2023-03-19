using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Services.DTOs.Partner;

namespace Licenta.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Partner, PartnerViewDTO>().ReverseMap();
    }
}
