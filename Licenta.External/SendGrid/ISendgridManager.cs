namespace Licenta.External.SendGrid;

public interface ISendgridManager
{
    Task SendEmailTemplate(List<SendgridUser> emailDtos);
    Task<string> CreateHtml();
}
