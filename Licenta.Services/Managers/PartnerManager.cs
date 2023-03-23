using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Partner;
using Licenta.Services.Exceptions;
using Licenta.Services.Interfaces;
using Licenta.Services.QueryParameters;
using Licenta.Services.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

    public async Task<PartnerViewDTO> AddAsync(PartnerPostDTO partnerDto)
    {
        var partner = _mapper.Map<Partner>(partnerDto);

        await _partnerRepository.AddAsync(partner);

        return _mapper.Map<PartnerViewDTO>(partner);
    }

    public async Task<PagedList<PartnerViewDTO>> ListPartnersAsync(PartnerParameters parameters)
    {
        var spec = new PartnerSpecification(parameters);
        var partners = await _partnerRepository.FindBySpecAsync(spec);
        return _mapper.Map<PagedList<PartnerViewDTO>>(partners);
    }

    public async Task<PartnerViewDTO> GetPartnerProfileByIdAsync(long id)
    {
        var partner = await _partnerRepository
            .AsQueryable()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (partner == null)
        {
            throw new CustomNotFoundException("Partner Not Found");
        }
        return _mapper.Map<PartnerViewDTO>(partner);
    }

    public async Task<PartnerViewDTO> UpdateAsync(PartnerPutDTO partnerDto)
    {
        var partner = await _partnerRepository.FindByIdAsync(partnerDto.Id);

        if (partner == null)
        {
            throw new CustomNotFoundException("Partner Not Found");
        }

        _mapper.Map(partnerDto, partner);
        await _partnerRepository.UpdateAsync(partner);

        return await GetPartnerProfileByIdAsync(partnerDto.Id);
    }

    public async Task DeleteAsync(long id)
    {
        var partner = await _partnerRepository.FindByIdAsync(id);
        if (partner == null)
        {
            throw new CustomNotFoundException("Partner Not Found");
        }
        await _partnerRepository.RemoveAsync(partner);
    }
}
