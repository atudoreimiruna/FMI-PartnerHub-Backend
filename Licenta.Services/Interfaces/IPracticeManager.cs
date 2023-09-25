using Licenta.Services.DTOs.Practice;
using System.Threading.Tasks;

namespace Licenta.Services.Interfaces;

public interface IPracticeManager
{
    Task<PracticeViewDTO> GetPracticeByIdAsync(long id);
    Task<PracticeViewDTO> UpdateAsync(PracticePutDTO practiceDto);
}
