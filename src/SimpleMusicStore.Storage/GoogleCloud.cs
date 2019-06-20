using Google.Cloud.Storage.V1;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Google.Apis.Auth.OAuth2;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Services;
using SimpleMusicStore.Constants;
using SimpleMusicStore.Contracts.Validators;

namespace SimpleMusicStore.Storage
{
    public class GoogleCloud : FileStorage
    {
        private readonly StorageClient _storage;
        private readonly IServiceValidator _validator;

        public GoogleCloud(IServiceValidator validator)
        {
            //TODO move credentials file
            _storage = StorageClient.Create();
            _validator = validator;
        }

        public async Task Upload(IFormFile file, string fileName)
        {
            await _storage.UploadObjectAsync(
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
