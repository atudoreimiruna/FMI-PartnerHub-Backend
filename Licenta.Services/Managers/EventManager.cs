using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Extensions.PagedList;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Event;
using Licenta.Services.DTOs.Partner;
using Licenta.Services.Exceptions;
using Licenta.Services.Interfaces;
using Licenta.Services.QueryParameters;
using Licenta.Services.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class EventManager : IEventManager
{
    private readonly IRepository<Event> _eventRepository;
    private readonly IMapper _mapper;

    public EventManager(
        IRepository<Event> eventRepository,
        IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<EventViewDTO> AddAsync(EventPostDTO eventDto, string partnerId)
    {
        if (partnerId != null && eventDto.PartnerId == long.Parse(partnerId)) 
        {
            var result = _mapper.Map<Event>(eventDto);

            await _eventRepository.AddAsync(result);

            return _mapper.Map<EventViewDTO>(result);
        }
        else
        {
            throw new CustomNotFoundException("The event cannot be added");
        }
    }

    public async Task<PagedList<EventViewDTO>> ListEventsAsync(EventParameters parameters)
    {
        var spec = new EventSpecification(parameters);
        var events = await _eventRepository.FindBySpecAsync(spec);
        return _mapper.Map<PagedList<EventViewDTO>>(events);
    }

    public async Task<EventViewDTO> GetEventProfileByIdAsync(long id)
    {
        var result = await _eventRepository
            .AsQueryable()
            .Include(x => x.Partner)
            .Include(x => x.Files)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (result == null)
        {
            throw new CustomNotFoundException("Event Not Found");
        }
        return _mapper.Map<EventViewDTO>(result);
    }

    public async Task<EventViewDTO> UpdateAsync(EventPutDTO eventDto, string partnerId)
    {
        if (partnerId != null && eventDto.Id == long.Parse(partnerId))
        {
            var result = await _eventRepository.FindByIdAsync(eventDto.Id);

            if (result == null)
            {
                throw new CustomNotFoundException("Event Not Found");
            }

            _mapper.Map(eventDto, result);

            if (eventDto.Date != null)
            {
                result.Date = eventDto.Date.Value;
            }

            await _eventRepository.UpdateAsync(result);

            return await GetEventProfileByIdAsync(eventDto.Id);
        }
        else
        {
            throw new CustomNotFoundException("The event cannot be updated");
        }
    }

    public async Task DeleteAsync(long id, string partnerId)
    {
        var result = await _eventRepository.FindByIdAsync(id);
        if (result == null)
        {
            throw new CustomNotFoundException("Event Not Found");
        }
        if (partnerId != null && result.PartnerId == long.Parse(partnerId))
        {
            await _eventRepository.RemoveAsync(result);
        }
        else
        {
            throw new CustomNotFoundException("The event cannot be deleted");
        }
    }
}
