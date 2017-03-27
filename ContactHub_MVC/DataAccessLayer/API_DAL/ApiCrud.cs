using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using ContactHub_MVC.Helper;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ContactHub_MVC.CommonData.Constants;

namespace ContactHub_MVC.DataAccessLayer.API_DAL
{
    public class ApiCrud<TReq, TRes> : IApiCrud<TReq, TRes>
        where TRes : class
        where TReq : class
    {
        private string BaseUrl => @"http://localhost:1820/api";
        private HttpClient ApiClient { get; set; }
        public ApiCrud()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var userIdentity = (ClaimsIdentity)Thread.CurrentPrincipal.Identity;
            if (userIdentity.Claims.Count() > default(int))
            {
                var token = userIdentity.Claims.SingleOrDefault(x => x.Type == ClaimTypes.Authentication).Value;
                ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            }
        }
        Task<TRes> IApiCrud<TReq, TRes>.Delete(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.DeleteAsync($"{BaseUrl}/{apiUrl}").Result;
            var result = JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Get(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.GetAsync($"{BaseUrl}/{apiUrl}").Result;
            var result = JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Post(TReq model, string apiUrl, HttpContentTypes? contentType)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.PostAsync($"{BaseUrl}/{apiUrl}",Utility.GetContent(model, contentType).Result).Result;
            var result = JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Put(TReq model, string apiUrl, HttpContentTypes? contentType)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.PutAsync($"{BaseUrl}/{apiUrl}", Utility.GetContent(model,contentType).Result).Result;
            var result = JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(null);
        }
    }
}
