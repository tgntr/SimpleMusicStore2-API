using Google.Cloud.Storage.V1;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Google.Apis.Auth.OAuth2;
using SimpleMusicStore.Contracts;
using SimpleMusicStore.Contracts.Services;

namespace SimpleMusicStore.Storage
{
    public class GoogleCloud : FileStorage
    {
        private const string BUCKET_NAME = "simplemusicstore";

        private readonly StorageClient _storage;
        private readonly IServiceValidations _validator;

        public GoogleCloud(IServiceValidations validator)
        {
            //TODO move credentials file
            _storage = StorageClient.Create();
            _validator = validator;
        }

        public async Task<string> Upload(IFormFile file, string fileName)
        {
            //TODO move this validation where it belongs
            _validator.FileIsMP3(file.ContentType);

            var track = await _storage.UploadObjectAsync(
                bucket: BUCKET_NAME,
                objectName: fileName,
                contentType: file.ContentType,
                source: file.OpenReadStream(),
                options: PublicReadAccess()
            );

            return track.MediaLink;
        }

        private UploadObjectOptions PublicReadAccess()
        {
            return new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead };
        }

		//public async Task Delete(string fileName)
		//{
		//    try
		//    {
		//        await _storage.DeleteObjectAsync(BUCKET_NAME, fileName);
		//    }
		//    catch (Google.GoogleApiException exception)
		//    {
		//        // A 404 error is ok.  The image is not stored in cloud storage.
		//        if (exception.Error.Code != 404)
		//            throw;
		//    }
		//}
	}
}
