﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.Models.MailingModel
{
    public class EmailCredentialViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSsl { get; set; }
    }
}
