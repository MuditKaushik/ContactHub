using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContactHub_MVC.Models.UserModel
{
    public partial class SyncContacts
    {
        [Display(Name = "PhoneNumber",ResourceType =typeof(MessageResource))]
        public string PhoneNumber { get; set; }
        [Display(Name = "ContactMode", ResourceType = typeof(MessageResource))]
        public string ContactMode { get; set; }

    }
    public partial class SyncContacts
    {
        public IEnumerable<ContactDetails> ContactList { get; set; }
        public IEnumerable<SelectListItem> ContactModeList { get; set; }
    }
}
