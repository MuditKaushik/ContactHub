using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.DataAccessLayer.API_DAL
{
    public class ApiCrud<TRes, TReq> : IApiCrud<TReq, TRes>
        where TRes : class
        where TReq : class
    {
        var client = new HttpClient();
        public ApiCrud()
        {

        }
        Task<TRes> IApiCrud<TReq, TRes>.Delete(TReq model)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            return Task.FromResult<TRes>(null);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Get(TReq model)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            return Task.FromResult<TRes>(null);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Post(TReq model)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            return Task.FromResult<TRes>(null);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Put(TReq model)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            return Task.FromResult<TRes>(null);
        }
    }
}
