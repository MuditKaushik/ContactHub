using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.DataAccessLayer.API_DAL
{
    public class AccessAPI<TReq,TRes>
        where TRes: class
        where TReq: class
    {
        private IApiCrud<TRes, TReq> ApiCaller;
        public AccessAPI()
        {
            ApiCaller = new ApiCrud<TReq, TRes>();
        }
        public TRes AuthenticateUser(TReq model)
        {
            return null;
        }
    }
}
