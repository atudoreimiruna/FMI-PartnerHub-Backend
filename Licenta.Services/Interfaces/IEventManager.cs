using Licenta.Core.Extensions.PagedList;
using Licenta.Services.DTOs.Event;
using Licenta.Services.QueryParameters;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IEventManager
{
    Task<EventViewDTO> AddAsync(EventPostDTO eventDto, string partnerId);
    Task<PagedList<EventViewDTO>> ListEventsAsync(EventParameters parameters);
    Task<EventViewDTO> GetEventProfileByIdAsync(long id);
    Task<EventViewDTO> UpdateAsync(EventPutDTO eventDto, string partnerId);
    Task DeleteAsync(long id, string partnerId);
}
