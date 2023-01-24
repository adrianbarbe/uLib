namespace RemoteFinder.BLL.Services.AwsMinioClient
{
    public class AwsMinioGetFileResponse
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public MemoryStream Stream { get; set; }
    }
}