using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.Models.MailingModel
{
    public class EmailCredentialViewModel
    {
        public Credentials Credentials { get; set; }
        public SmtpEssentials SmtpEssentials { get; set; }
        public MailFeilds MailFeilds { get; set; }

    }
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class SmtpEssentials
    {
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSsl { get; set; }
    }
    public class MailFeilds
    {
        public string From { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
