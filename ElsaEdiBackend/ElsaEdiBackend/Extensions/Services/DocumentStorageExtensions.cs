using ElsaEdiBackend.Services;
using Storage.Net;
using Storage.Net.Blobs;

namespace ElsaEdiBackend.Extensions.Services
{
    public static class DocumentStorageExtensions
    {
        public static void AddDocumentStorage(this IServiceCollection services, string contentRootPath)
        {
            services.Configure<DocumentStorageOptions>(options => options.BlobStorageFactory = () => StorageFactory.Blobs.DirectoryFiles(Path.Combine(contentRootPath, "App_Data/Uploads")));
            services.AddSingleton<IFileStorage, FileStorage>();
        }
    }

    public class DocumentStorageOptions
    {
        public Func<IBlobStorage> BlobStorageFactory { get; set; } = StorageFactory.Blobs.InMemory;
    }
}