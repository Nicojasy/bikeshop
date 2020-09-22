using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace bikeshop.EmailServices.EmailService
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
