using System;
using System.Collections.Generic;
using System.Text;

namespace BOOKSTore.Email
{
    public interface IEmailService
    {
        bool SendEmail(EmailData emailData);
    }
}
