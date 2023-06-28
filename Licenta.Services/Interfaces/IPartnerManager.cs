using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Partner;
using Licenta.Services.QueryParameters;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IPartnerManager
{
    Task<PartnerViewDTO> AddAsync(PartnerPostDTO partnerDto);
    Task<PagedList<PartnerViewDTO>> ListPartnersAsync(PartnerParameters parameters);
    Task<PartnerViewDTO> GetPartnerProfileByIdAsync(long id);
    Task<PartnerViewDTO> UpdateAsync(PartnerPutDTO partnerDto, string partnerId);
    Task DeleteAsync(long id);
    Task DeleteAsync(PartnerDeleteDTO partnerDto);
}
