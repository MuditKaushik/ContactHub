using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactHub_MVC.CommonData.Constants;

namespace ContactHub_MVC.Models.UserModel
{
    public class DownloadFileViewModel
    {
        public string FileName { get; set; }
        public FileType FileType { get; set; }
        public List<int> ContactIds { get; set; }
        public bool IsFileCreated { get; set; }
    }
}
