using System;
using System.Threading.Tasks;

namespace AsyncTest.Communication.Client.Communication
{
    public interface IRestClient
    {
        Task<T> GetAsync<T>(Uri uri);
        Task DeleteAsync(Uri uri);
    }
}