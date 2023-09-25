using System.Threading.Tasks;

namespace Licenta.Services.Interfaces.External;

public interface IHangfireManager
{
   Task SendMonthlyEmail();
}
