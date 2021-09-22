using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using StorageLibrary.Configuration;
using StorageLibrary.Exceptions;
using StorageLibrary.Models;
using StorageLibrary.Providers;

namespace StorageLibrary.Services
{
    public class StorageService : IStorageService
    {
        private readonly ILogService _logService;
        private readonly IDataStorageService _dataService;
        private readonly ICustomConfigurationProvider _configurationProvider;
        private readonly ITimeProvider _timeProvider;

        public StorageService(ILogService logService, IDataStorageService dataService,
            ICustomConfigurationProvider configurationProvider, ITimeProvider timeProvider)
        {
            _logService = logService;
            _dataService = dataService;
            _configurationProvider = configurationProvider;
            _timeProvider = timeProvider;
        }

        public async Task StoreData(PublicApiResponse response)
        {
            var id = Guid.NewGuid().ToString().Replace("-", "");

            if (response.Successful)
            {
                try
                {
                    await _dataService.UploadContentAsync(response.Payload, id);
                }
                catch (Exception e)
                {
                    throw new DataStorageException(e.Message);
                }

                try
                {
                    await _logService.AddLogEntryAsync(id, response.StatusCode, _timeProvider.Now());
                }
                catch (Exception e)
                {
                    throw new LogStorageException(e.Message);
                }
            }
            else
            {
                try
                {
                    await _logService.AddLogEntryAsync(id, response.StatusCode, _timeProvider.Now());
                }
                catch (Exception e)
                {
                    throw new LogStorageException(e.Message);
                }
            }
        }

        public async Task<IEnumerable<string>> RetrieveLogEntries(DateTime startDate, DateTime endDate)
        {
            return await _logService.ListEntriesByDateAsync(startDate, endDate);
        }

        public async Task<string> RetrieveData(string id)
        {
            return await _dataService.GetDataAsync(id);
        }
    }
}