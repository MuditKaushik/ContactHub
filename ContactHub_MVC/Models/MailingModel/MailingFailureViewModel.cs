using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.Models.MailingModel
{
    public class MailingFailureViewModel
    {
        public List<string> EmailFailures { get; set; }
        public bool IsMailSent { get; set; }
    }
}
