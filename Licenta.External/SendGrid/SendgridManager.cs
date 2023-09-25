using Licenta.Core.Entities;
using Licenta.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Licenta.External.SendGrid;

public class SendgridManager : ISendgridManager
{
    private readonly IRepository<Partner> _partnerRepository;
    private readonly IRepository<Event> _eventRepository;
    private readonly IRepository<Job> _jobRepository;
    private IConfiguration Configuration { get; }
    public SendgridManager(IConfiguration configuration, 
        IRepository<Partner> partnerRepository,
        IRepository<Event> eventRepository,
        IRepository<Job> jobRepository)
    {
        Configuration = configuration;
        _partnerRepository = partnerRepository;
        _jobRepository = jobRepository;
        _eventRepository = eventRepository;
    }

    public async Task SendEmailTemplate(List<SendgridUser> emailDtos)
    {
        var apiKey = Configuration["SendGrid:SENDGRID_API_KEY"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Configuration["SendGrid:SENDGRID_EMAIL"], Configuration["SendGrid:SENDGRID_NAME"]);
        var subject = "Noutăți și evenimente în FMI PartnerHub";
        var plainTextContent = "Bună!";
        var htmlContent = await CreateHtml();

        foreach (var emailDto in emailDtos)
        {
            var to = new EmailAddress(emailDto.Email, emailDto.Name);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent.ToString());
            await client.SendEmailAsync(msg);
        }
    }

    public async Task<string> CreateHtml()
    {
        var currentDateTime = DateTime.UtcNow;
        var pastDateTime = DateTime.UtcNow.AddMonths(-1);
        var futureDateTime = DateTime.UtcNow.AddMonths(1);

        string emailBody = "";

        // partners
        string bodyHtml1 = await System.IO.File.ReadAllTextAsync("Resources/PartnersEmail.html");
        bodyHtml1 = bodyHtml1.Replace("{ baseUrl }", Configuration["SendGrid:BaseUrl"]);

        var partners = await _partnerRepository
            .AsQueryable()
            .Where(x => x.CreatedAt <= currentDateTime && x.CreatedAt >= pastDateTime)
            .ToListAsync();

        if (partners.Count > 0)
        {
            foreach (var partner in partners)
            {
                bodyHtml1 = bodyHtml1
                    .Replace("{ partnerId }", partner.Id.ToString())
                    .Replace("{ partnerName }", partner.Name);
            }
            emailBody += bodyHtml1;
        }

        // jobs
        string bodyHtml2 = await System.IO.File.ReadAllTextAsync("Resources/JobsEmail.html");

        var jobs = await _jobRepository
            .AsQueryable()
            .Include(x => x.Partner)
            .Where(x => x.CreatedAt <= currentDateTime && x.CreatedAt >= pastDateTime)
            .ToListAsync();

        if (jobs.Count > 0)
        {
            foreach (var job in jobs)
            {
                bodyHtml2 = bodyHtml2
                    .Replace("{ jobName }", job.Title)
                    .Replace("{ jobPartnerName }", job.Partner.Name);
            }
            emailBody += bodyHtml2;
        }

        // events
        string bodyHtml3 = await System.IO.File.ReadAllTextAsync("Resources/EventsEmail.html");

        var events = await _eventRepository
            .AsQueryable()
            .Where(x => x.Date >= currentDateTime && x.Date <= futureDateTime)
            .ToListAsync();

        if (events.Count > 0)
        {
            foreach (var e in events)
            {
                bodyHtml3 = bodyHtml3
                    .Replace("{ EventName }", e.Title)
                    .Replace("{ EventLocation }", e.Location)
                    .Replace("{ EventDate }", e.Date.ToString("yyyy-MM-dd"))
                    .Replace("{ EventTime }", e.Time);
            }
            emailBody += bodyHtml3;
        }

        string bodyHtml4 = await System.IO.File.ReadAllTextAsync("Resources/FinalEmail.html");
        emailBody += bodyHtml4;

        return emailBody;
    }
}
