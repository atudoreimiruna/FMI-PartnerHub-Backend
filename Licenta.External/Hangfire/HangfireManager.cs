using Licenta.Core.Entities;
using Licenta.Core.Interfaces;
using Licenta.External.SendGrid;
using Licenta.Services.Interfaces.External;
using Microsoft.EntityFrameworkCore;

namespace Licenta.External.Hangfire;

public class HangfireManager : IHangfireManager 
{
    private readonly ISendgridManager _sendgridManager;
    private readonly IRepository<Student> _studentRepository;
    private readonly IRepository<UserRole> _userRoleRepository;
   
    public HangfireManager(ISendgridManager sendgridManager, 
        IRepository<Student> studentRepository,
        IRepository<UserRole> userRoleRepository)
    {
        _sendgridManager = sendgridManager;
        _studentRepository = studentRepository;
        _userRoleRepository = userRoleRepository;
    }

    public async Task SendMonthlyEmail()
    {
        var emailDtos = new List<SendgridUser>();
        var students = await _studentRepository
            .AsQueryable()
            .ToListAsync();
        foreach(var student in students)
        {
            emailDtos.Add(new SendgridUser { Email = student.Email, Name = student.Name });
        }
        await _sendgridManager.SendEmailTemplate(emailDtos);
    }
}
