using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactHub_ApiCaller.ApiService;

namespace ContactHub_MVC.DataAccessLayer
{
    public class DAL<T> : Controller
        where T:class
    {
        protected IApiCallerService<T> ApiManager { get; }
        public DAL()
        {
            ApiManager = new ApiCallerService<T>();
        }
    }
}