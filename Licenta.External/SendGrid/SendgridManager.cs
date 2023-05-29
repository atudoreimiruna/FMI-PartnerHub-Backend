using Licenta.Services.Interfaces.External;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace Licenta.External.SendGrid;

public class SendgridManager : ISendgridManager
{
    public IConfiguration Configuration { get; }
    public SendgridManager(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public async Task SendEmailTemplate(SendgridUser emailDto)
    {
        var apiKey = Configuration["SendGrid:SENDGRID_API_KEY"];

        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(Configuration["SendGrid:SENDGRID_EMAIL"], Configuration["SendGrid:SENDGRID_NAME"]);
        var to = new EmailAddress(emailDto.Email, emailDto.Name);
        var subject = "Thank you for your register!";
        var plainTextContent = "Hi!";
        var htmlContent = "<p><span style=\"font-family:Lucida Sans Unicode,Lucida Grande,sans-serif\"><span style=\"font-size:16px\">Hello, thank you for registering on Bookgram!&nbsp;</span></p>" +
            "<p><span style=\"font-family:Lucida Sans Unicode,Lucida Grande,sans-serif\">BookGram is the right place for your reading! You will be able to post your favorite books, you will have weekly challenges and you will be able to make friends who love reading.</span></p>" +
            "<p>&nbsp;</p>";
        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            plainTextContent,
            htmlContent
       );
       var response = await client.SendEmailAsync(msg);
    }
}
