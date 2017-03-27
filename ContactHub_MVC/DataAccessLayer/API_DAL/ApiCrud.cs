using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;

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
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }
        async Task<TRes> IApiCrud<TReq, TRes>.Delete(TReq model,string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var response = await ApiClient.DeleteAsync($"{BaseUrl}/{apiUrl}");
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        async Task<TRes> IApiCrud<TReq, TRes>.Get(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var response = await ApiClient.GetAsync($"{BaseUrl}/{apiUrl}");
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        async Task<TRes> IApiCrud<TReq, TRes>.Post(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var httpContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await ApiClient.PostAsync($"{BaseUrl}/{apiUrl}", httpContent);
            var result = JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        async Task<TRes> IApiCrud<TReq, TRes>.Put(TReq model, string apiUrl)
        {
            if (model == null)
                throw new ArgumentNullException("Object null");
            var jsonModel = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await ApiClient.PutAsync($"{BaseUrl}/{apiUrl}", jsonModel);
            var result = (!response.IsSuccessStatusCode) ? null : JsonConvert.DeserializeObject<TRes>(response.Content.ReadAsStringAsync().Result);
            return result;
        }

        private HttpContent CreateHttpContent(TReq model)
        {
            var content = JsonConvert.SerializeObject(model);
            var contentBytes = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(contentBytes);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
            //return new StringContent(JsonConvert.SerializeObject(model));
            //return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }

    }
}
