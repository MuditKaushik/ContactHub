using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace ContactHub_MVC.DataAccessLayer.API_DAL
{
    public class ApiCrud<TReq, TRes> : IApiCrud<TReq, TRes>
        where TRes : class
        where TReq : class
    {
        private string BaseUrl => @"http://localhost:12345/api";
        private HttpClient ApiClient { get; set; }
        public ApiCrud()
        {
            ApiClient = new HttpClient();
            ApiClient.BaseAddress = new Uri(BaseUrl);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        Task<TRes> IApiCrud<TReq, TRes>.Delete(TReq model,string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.DeleteAsync(apiUrl).Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Get(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.GetAsync(apiUrl).Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Post(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var jsonModel = new StringContent(JsonConvert.SerializeObject(model).ToString(),Encoding.UTF8,"application/json");
            HttpResponseMessage response = ApiClient.PostAsync(apiUrl,jsonModel).Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(null);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Put(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var jsonModel = new StringContent(JsonConvert.SerializeObject(model).ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = ApiClient.PutAsync(apiUrl,jsonModel).Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(null);
        }
    }
}
