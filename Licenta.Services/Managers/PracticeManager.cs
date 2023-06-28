using AutoMapper;
using Licenta.Core.Entities;
using Licenta.Core.Interfaces;
using Licenta.Services.DTOs.Job;
using Licenta.Services.DTOs.Practice;
using Licenta.Services.Exceptions;
using Licenta.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Licenta.Services.Managers;

public class PracticeManager : IPracticeManager
{
    private readonly IRepository<Practice> _practiceRepository;
    private readonly IMapper _mapper;

    public PracticeManager(
        IRepository<Practice> practiceRepository,
        IMapper mapper)
    {
        _practiceRepository = practiceRepository;
        _mapper = mapper;
    }

    public async Task<PracticeViewDTO> GetPracticeByIdAsync(long id)
    {
        var practice = await _practiceRepository
            .AsQueryable()
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();

        if (practice == null)
        {
            throw new CustomNotFoundException("Practice Not Found");
        }
        return _mapper.Map<PracticeViewDTO>(practice);
    }

    public async Task<PracticeViewDTO> UpdateAsync(PracticePutDTO practiceDto)
    {
      
        var practice = await _practiceRepository.FindByIdAsync(practiceDto.Id);

        if (practice == null)
        {
            throw new CustomNotFoundException("Practice Not Found");
        }

        _mapper.Map(practiceDto, practice);

        await _practiceRepository.UpdateAsync(practice);

        return await GetPracticeByIdAsync(practiceDto.Id);
 
    }

}
