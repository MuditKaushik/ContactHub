using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_MVC.DataAccessLayer.API_DAL
{
    public interface IApiCrud<TReq,TRes>
        where TReq:class
        where TRes:class
    {
        Task<TRes> Get(TReq model,string apiUrl);
        Task<TRes> Put(TReq model, string apiUrl);
        Task<TRes> Post(TReq model, string apiUrl);
        Task<TRes> Delete(TReq model, string apiUrl);
    }
}
