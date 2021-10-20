using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts;
using System.Threading.Tasks;

namespace SimpleMusicStore.Storage
{
    public class GoogleCloud : FileStorage
    {
        private readonly StorageClient _storage;

        public GoogleCloud()
        {
            //TODO move credentials file
            _storage = StorageClient.Create();
        }

        public Task Upload(IFormFile file, string fileName)
        {
            return _storage.UploadObjectAsync(
                    bucket: CommonConstants.BUCKET_NAME,
                    objectName: fileName,
                    contentType: file.ContentType,
                    source: file.OpenReadStream(),
                    options: PublicReadAccess()
                );

        }

        private UploadObjectOptions PublicReadAccess()
        {
            return new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead };
        }
    }
}
