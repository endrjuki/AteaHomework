using System;
using StorageLibrary.Exceptions;

namespace StorageLibrary.Services
{
    public class ApiScraper : IApiScraper
    {
        private readonly IApiFetchService _apiFetchService;
        private readonly IStorageService _storageService;

        public ApiScraper(IApiFetchService apiFetchService, IStorageService storageService)
        {
            _apiFetchService = apiFetchService;
            _storageService = storageService;
        }

        public async void Scrape()
        {
            try
            {
                var response = await _apiFetchService.FetchData();
                try
                {
                    await _storageService.StoreData(response);
                }
                catch (DataStorageException ex)
                {
                    throw;
                }
                catch (LogStorageException ex)
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}