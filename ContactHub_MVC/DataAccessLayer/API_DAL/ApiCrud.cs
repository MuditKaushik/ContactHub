using System;
using System.Linq;
using System.Text;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Collections.Generic;
using ContactHub_MVC.Helper;
using ContactHub_MVC.CommonData.Constants;

namespace ContactHub_MVC.DataAccessLayer.API_DAL
{
    public class ApiCrud<TReq, TRes> : IApiCrud<TReq, TRes>
        where TRes : class
        where TReq : class
    {
        private string BaseUrl => @"http://localhost:7333/api";
        private HttpClient ApiClient { get; set; }
        public ApiCrud()
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.DefaultRequestHeaders.Authorization = 
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

        Task<TRes> IApiCrud<TReq, TRes>.Post(TReq model, string apiUrl, HttpContentTypes? contentType)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            HttpResponseMessage response = ApiClient.PostAsync($"{BaseUrl}/{apiUrl}", GetContent(model, contentType).Result).Result;
            var result = JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(result);
        }

        Task<TRes> IApiCrud<TReq, TRes>.Put(TReq model, string apiUrl, HttpContentTypes? contentType)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var jsonModel = new StringContent(JsonConvert.SerializeObject(model).ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage response = ApiClient.PutAsync($"{BaseUrl}/{apiUrl}", jsonModel).Result;
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return Task.FromResult<TRes>(null);
        }

        private async Task<dynamic> GetContent(TReq model,HttpContentTypes? contentType)
        {
            switch (contentType)
            {
                case HttpContentTypes.ConvertToEncodedUrl:
                    var DictionaryModel = Utility.CovertToDictionary(model).Result;
                    var FormUrlEncodedContent = new FormUrlEncodedContent(DictionaryModel);
                    return await Task.FromResult(FormUrlEncodedContent);
                default: return model;
            }
        }
    }
}
