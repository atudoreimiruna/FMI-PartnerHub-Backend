namespace Licenta.External.SendGrid;

public interface ISendgridManager
{
    Task SendEmailTemplate(SendgridUser emailDto);
}
