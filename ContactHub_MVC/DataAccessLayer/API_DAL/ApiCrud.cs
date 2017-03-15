using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using ContactHub_MVC.Helper;

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
        }
        Task<TRes> IApiCrud<TReq, TRes>.Delete(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.DeleteAsync($"{BaseUrl}/{apiUrl}").Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Get(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.GetAsync($"{BaseUrl}/{apiUrl}").Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Post(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var Content = Utility.ConvertRequestObjectToHttpContent(model).Result;
            HttpResponseMessage response = ApiClient.PostAsync($"{BaseUrl}/{apiUrl}", Content).Result;
            var result = JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Put(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var jsonModel = new StringContent(JsonConvert.SerializeObject(model).ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = ApiClient.PutAsync($"{BaseUrl}/{apiUrl}", jsonModel).Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(null);
        }
    }
}
