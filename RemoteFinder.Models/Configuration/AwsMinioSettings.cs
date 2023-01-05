namespace RemoteFinder.Models.Configuration
{
    public class AwsMinioSettings
    {
        public string MinioInstanceUrl { get; set; }
        public string AccessKey { get; set; }
        
        public string SecretKey { get; set; }
    }
}