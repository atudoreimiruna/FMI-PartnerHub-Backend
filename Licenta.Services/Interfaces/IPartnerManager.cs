using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Partner;
using Licenta.Services.QueryParameters.Partner;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IPartnerManager
{
    Task<PagedList<PartnerViewDTO>> ListPartnersAsync(PartnerParameters parameters);
}
