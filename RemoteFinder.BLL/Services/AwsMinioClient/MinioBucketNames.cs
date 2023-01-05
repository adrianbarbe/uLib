namespace RemoteFinder.BLL.Services.AwsMinioClient
{
    public static class MinioBucketNames
    {
/*        Bucket name should conform with DNS requirements: 
        - Should not contain uppercase characters
        - Should not contain underscores (_)
        - Should be between 3 and 63 characters long
        - Should not end with a dash
        - Cannot contain two, adjacent periods
        - Cannot contain dashes next to periods (e.g., "my-.bucket.com" and "my.-bucket" are invalid)
*/
    
        public const string UlibBooks = "ulib-books";
    }
}