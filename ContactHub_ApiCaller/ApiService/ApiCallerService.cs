using System;
using System.Net.Http;
using System.Runtime;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ContactHub_ApiCaller.ApiService
{
    public class ApiCallerService<T>: IApiCallerService<T>
        where T:class
    {
        private HttpClient _apiCaller;
        private readonly string baseUrl = "";
        private readonly string mediaType = "application/json";
        public ApiCallerService()
        {
            _apiCaller = new HttpClient();
            _apiCaller.BaseAddress = new Uri(baseUrl);
            _apiCaller.DefaultRequestHeaders.Accept.Clear();
            _apiCaller.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(mediaType));
        }
        Task<IEnumerable<T>> IApiCallerService<T>.Get(IEnumerable<string> id)
        {
            throw new NotImplementedException();
        }

        Task<T> IApiCallerService<T>.Put(T obj)
        {
            throw new NotImplementedException();
        }

        Task<T> IApiCallerService<T>.Post(T obj)
        {
            throw new NotImplementedException();
        }

        Task<T> IApiCallerService<T>.Delete(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
