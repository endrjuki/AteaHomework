using System.Threading.Tasks;
using Refit;
using StorageLibrary.Models;

namespace StorageLibrary.Services
{
    public interface IPublicApi
    {
        [Get("/random?auth=null")]
        Task<PublicApiResponse> FetchData();
    }
}