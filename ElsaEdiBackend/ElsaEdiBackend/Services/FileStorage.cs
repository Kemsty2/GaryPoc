using ElsaEdiBackend.Extensions.Services;
using Microsoft.Extensions.Options;
using Storage.Net.Blobs;

namespace ElsaEdiBackend.Services
{
    public interface IFileStorage 
    {
        Task WriteAsync(byte[] bytes, string filePath, CancellationToken cancellationToken = default);

        Task<Stream> ReadAsync(string fileName, CancellationToken cancellationToken = default);

        Task DeleteAsync(string fileName, CancellationToken cancellationToken = default);
    }

    public class FileStorage : IFileStorage
    {
        private readonly IBlobStorage _blobStorage;

        public FileStorage(IOptions<DocumentStorageOptions> options)
        {
            _blobStorage = options.Value.BlobStorageFactory();
        }

        public Task<Stream> ReadAsync(string fileName, CancellationToken cancellationToken = default)
        {
            return _blobStorage.OpenReadAsync(fileName, cancellationToken);
        }

        public Task WriteAsync(byte[] bytes, string filePath, CancellationToken cancellationToken = default)
        {
            return _blobStorage.WriteAsync(filePath, bytes, false, cancellationToken);
        }

        public Task DeleteAsync(string fileName, CancellationToken cancellationToken = default)
        {
            return _blobStorage.DeleteAsync(fileName, cancellationToken);
        }
    }
}