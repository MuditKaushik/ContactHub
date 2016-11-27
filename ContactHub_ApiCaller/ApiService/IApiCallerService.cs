using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHub_ApiCaller.ApiService
{
    public interface IApiCallerService<T>
        where T : class
    {
        Task<IEnumerable<T>> Get(IEnumerable<string> id);
        Task<T> Put(T obj);
        Task<T> Post(T obj);
        Task<T> Delete(T obj);
    }
}
