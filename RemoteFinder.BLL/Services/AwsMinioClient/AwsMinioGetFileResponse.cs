using System.IO;

namespace ePlato.CoreApp.BLL.AwsMinioClient
{
    public class AwsMinioGetFileResponse
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public MemoryStream Stream { get; set; }
    }
}