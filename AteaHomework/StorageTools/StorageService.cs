using System.Collections;
using System.Globalization;
using System.Threading.Tasks;

namespace StorageTools
{
    public class StorageService
    {
        private readonly IBlobService _blobService;
        private readonly ITableService _tableService;

        public StorageService(IBlobService blobService, ITableService tableService)
        {
            _blobService = blobService;
            _tableService = tableService;
        }

        public async Task Upload(string content, string filename)
        {
            //_blobService.UploadContentBlobAsync(content, filename);
        }
    }
}