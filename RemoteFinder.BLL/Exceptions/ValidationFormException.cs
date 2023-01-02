using RemoteFinder.Models.Shared;

namespace RemoteFinder.BLL.Exceptions
{
    public class ValidationFormException : Exception
    {
        public ValidationFormException(List<FormValidationInfo> error)
        {
            Errors = error;
        }

        public List<FormValidationInfo> Errors { get; }
    }
}