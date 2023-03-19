using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Partner;
using Licenta.Services.Interfaces;
using Licenta.Services.QueryParameters.Partner;
using Licenta.Services.Specifications;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class PartnerManager : IPartnerManager
{
    private readonly IRepository<Partner> _partnerRepository;
    private readonly IMapper _mapper;

    public PartnerManager(
        IRepository<Partner> partnerRepository, 
        IMapper mapper)
    {
        _partnerRepository = partnerRepository;
        _mapper = mapper;
    }

    public async Task<PagedList<PartnerViewDTO>> ListPartnersAsync(PartnerParameters parameters)
    {
        var spec = new PartnerSpecification(parameters);
        var partners = await _partnerRepository.FindBySpecAsync(spec);
        return _mapper.Map<PagedList<PartnerViewDTO>>(partners);
    }
}
