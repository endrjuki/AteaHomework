using System.Threading.Tasks;
using Refit;
using StorageLibrary.Models;

namespace StorageLibrary.Services
{
    public interface IApiFetchService
    {
        Task<PublicApiResponse> FetchData();
    }
}