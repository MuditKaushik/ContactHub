using System.Threading.Tasks;
using ContactHub_MVC.DataAccessLayer.API_DAL;

namespace ContactHub_MVC.DataAccessLayer
{
    public static class AccessAPI<TReq, TRes>
        where TRes : class
        where TReq : class
    {
        private static IApiCrud<TReq, TRes> ApiCaller = new ApiCrud<TReq, TRes>();
        public static async Task<TRes> AuthenticateUser(TReq model,string apiUrl)
        {
            return await ApiCaller.Post(model, apiUrl);
        }
        public static async Task<TRes> FindUser(TReq model,string apiUrl)
        {
            return await ApiCaller.Get(model, apiUrl);
        }
        public static async Task<TRes> RegisterUser(TReq model,string apiUrl)
        {
            return await ApiCaller.Post(model, apiUrl);
        }
    }
}
