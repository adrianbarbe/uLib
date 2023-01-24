using System.Net;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Microsoft.Extensions.Options;
using RemoteFinder.BLL.Exceptions;
using RemoteFinder.BLL.Extensions;
using RemoteFinder.BLL.Helpers;
using RemoteFinder.Models.Configuration;
using Serilog;

namespace RemoteFinder.BLL.Services.AwsMinioClient
{
    public interface IAwsMinioClient
    {
        Task<string> Upload(string bucketName, MemoryStream fileToUpload, string fileNameWithExt);

        Task<AwsMinioGetFileResponse> Get(string bucketName, string fileNameKey);

        Task Remove(string bucketName, string fileName);
    }

    public class AswMinioClient : IAwsMinioClient
    {
        private readonly AwsMinioSettings _awsMinioSettings;

        public readonly AmazonS3Client _client;

        public AswMinioClient(IOptions<AwsMinioSettings> awsMinioSettings)
        {
            _awsMinioSettings = awsMinioSettings.Value;

            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast1, // MUST set this before setting ServiceURL and it should match the `MINIO_REGION` enviroment variable.
                ServiceURL = _awsMinioSettings.MinioInstanceUrl,
                ForcePathStyle = true, // MUST be true to work correctly with MinIO server
            };
            
            _client = new AmazonS3Client(_awsMinioSettings.AccessKey, _awsMinioSettings.SecretKey, config);
        }

        public async Task<string> Upload(string bucketName, MemoryStream fileToUpload, string fileNameWithExt)
        {
            if (!Path.HasExtension(fileNameWithExt))
            {
                throw new UploadFileException("File doesn't have extension");
            }
            
            var fileExtension = Path.GetExtension(fileNameWithExt);
            var fileMimeType = MimeTypes.GetMimeType(fileExtension);
            var fileNameWithoutExt = fileNameWithExt.Replace(fileExtension, string.Empty);

            var fileNameWithoutExtTranslited = TransliterationHelper.Transliterate(fileNameWithoutExt.RemoveWhitespace());

            var uniqueFilename = $"{GetCurrentUnixTimeStamp()}__{fileNameWithoutExtTranslited.ToLower()}{fileExtension}";

            try
            {
                if (!await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName))
                {
                    var bucket = await _client.PutBucketAsync(bucketName);

                    if (bucket.HttpStatusCode != HttpStatusCode.OK)
                    {
                        throw new UploadFileException("Error when creating bucket");
                    }
                }

                var request = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = uniqueFilename,
                    ContentType = fileMimeType,
                    InputStream = fileToUpload,
                    CannedACL = S3CannedACL.AuthenticatedRead
                };

                var cancellationToken = new CancellationToken();

                var response = await _client.PutObjectAsync(request, cancellationToken);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    throw new UploadFileException("Error when uploading file");
                }
            }
            catch (AmazonS3Exception exception)
            {
                Log.ForContext<AswMinioClient>().Error("Cannot upload the file. Exception: {ExceptionMessage}", exception.Message);

                throw new UploadFileException($"Error when uploading file. Exception: {exception.Message}");
            }
            catch (Exception ex)
            {
                Log.ForContext<AswMinioClient>().Error("Cannot upload the file. Unknown exception: {ExMessage}", ex.Message);

                throw new UploadFileException($"Error when uploading file. Exception: {ex.Message}");
            }

            return uniqueFilename;
        }

        public async Task<AwsMinioGetFileResponse> Get(string bucketName, string fileNameKey)
        {
            try
            {
                var getRequest = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileNameKey,
                };

                var cancellationToken = new CancellationToken();

                var response = await _client.GetObjectAsync(getRequest, cancellationToken);

                var memoryStreamResponse = new MemoryStream();
                await response.ResponseStream.CopyToAsync(memoryStreamResponse);
                memoryStreamResponse.Position = 0;
                
                return new AwsMinioGetFileResponse
                {
                    Stream = memoryStreamResponse,
                    ContentType = response.Headers["Content-Type"],
                    FileName = fileNameKey,
                };
            }
            catch (AmazonS3Exception exception)
            {
                Log.ForContext<AswMinioClient>().Error("Cannot getting the file. Exception: {ExceptionMessage}", exception.Message);

                throw new UploadFileException($"Error when getting file. Exception: {exception.Message}");
            }
            catch (Exception ex)
            {
                Log.ForContext<AswMinioClient>().Error("Cannot getting the file. Unknown exception: {ExMessage}", ex.Message);

                throw new UploadFileException($"Error when getting file. Exception: {ex.Message}");
            }
        }

        public async Task Remove(string bucketName, string fileNameKey)
        {
            try
            {
                var deleteRequest = new DeleteObjectRequest
                {
                    BucketName = bucketName,
                    Key = fileNameKey,
                };

                var cancellationToken = new CancellationToken();

                var response = await _client.DeleteObjectAsync(deleteRequest, cancellationToken);

                if (response.HttpStatusCode != HttpStatusCode.NoContent)
                {
                    throw new UploadFileException("Error when uploading file");
                }
            }
            catch (AmazonS3Exception exception)
            {
                Log.ForContext<AswMinioClient>().Error("Cannot deleting the file. Exception: {ExceptionMessage}", exception.Message);

                throw new UploadFileException($"Error when deleting file. Exception: {exception.Message}");
            }
            catch (Exception ex)
            {
                Log.ForContext<AswMinioClient>().Error("Cannot deleting the file. Unknown exception: {ExMessage}", ex.Message);

                throw new UploadFileException($"Error when uploading file. Exception: {ex.Message}");
            }
        }

        private int GetCurrentUnixTimeStamp()
        {
            return (int) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}