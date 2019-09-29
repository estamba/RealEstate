using RealEstate.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Core.Interfaces.ExternalServices
{
    public interface IEmailSender
    {
        Task SendAsync(EmailModel emailModel);
    }
}
