using Google.Cloud.Storage.V1;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Google.Apis.Auth.OAuth2;

namespace StorageProviders
{
    public class GoogleCloud
    {
        private const string BUCKET_NAME = "simplemusicstore";
        private readonly StorageClient _storage;

        public GoogleCloud()
        {
            //TODO move credentials file
            var credentials = GoogleCredential.GetApplicationDefault();
            _storage = StorageClient.Create();
        }

        public async Task Upload(IFormFile file)
        {
            //TODO generate unique names because a file with the same name overrides the previous one
            await _storage.UploadObjectAsync(
                bucket: BUCKET_NAME,
                objectName: file.FileName,
                contentType: file.ContentType,
                source: file.OpenReadStream(),
                options: PublicReadAccess()
            );
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

        private UploadObjectOptions PublicReadAccess()
        {
            return new UploadObjectOptions { PredefinedAcl = PredefinedObjectAcl.PublicRead };
        }
    }
}
