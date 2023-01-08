using FluentValidation.Results;
using RemoteFinder.Models.Shared;

namespace RemoteFinder.BLL.Extensions;

public static class FormValidationExceptionExtensions
{
    public static List<FormValidationInfo> CreateErrorsList(this IList<ValidationFailure> errors)
    {
        return errors.Select(err => new FormValidationInfo
            {
                Field = err.PropertyName,
                Message = err.ErrorMessage,
            })
            .ToList();
    }
}