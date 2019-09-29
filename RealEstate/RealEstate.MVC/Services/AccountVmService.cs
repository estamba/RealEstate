using Microsoft.AspNetCore.Hosting;
using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstate.MVC.Services
{
    public class AccountVmService
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public AccountVmService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }
        public EmailModel GetPasswordRecoverEmailModel(string emailAddress, string url)
        {
            var path =  Path.Combine(Path.Combine(hostingEnvironment.WebRootPath, "email templates"), "password-recovery.html");
            return new EmailModel()
            {
                EmailAddress = emailAddress,
                Subject = "Forgot Your password",
                PlainContent = " test",
                HtmlContent = GetRecoveryPasswordHtmlContent(url, path)
            };
        }
        private string GetRecoveryPasswordHtmlContent(string url, string path)
        {
            string lines = File.ReadAllText(path);
            return lines.Replace("ResultpaasswordUrl", url);
        }
    }
}
