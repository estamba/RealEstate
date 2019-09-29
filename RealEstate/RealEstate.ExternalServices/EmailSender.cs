using Microsoft.Extensions.Configuration;
using RealEstate.Core.Entities;
using RealEstate.Core.Interfaces.ExternalServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace RealEstate.ExternalServices
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration configuration;

        public EmailSender(IConfiguration configuration )
        {
            this.configuration = configuration;
        }
        public async Task SendAsync(EmailModel emailModel)
        {
            var apiKey = configuration["SendGridConfig:ApiKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("tamba@kerrgi.com", "support@kerrgi.com");
            var subject = emailModel.Subject;
            var to = new EmailAddress("princearbby97@gmail.com", "Kerr Gi");
            var plainTextContent = emailModel.PlainContent;
            var htmlContent = emailModel.HtmlContent;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
