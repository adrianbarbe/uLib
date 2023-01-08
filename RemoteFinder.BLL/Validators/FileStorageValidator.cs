using FluentValidation;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Validators;

public class FileValidator : AbstractValidator<FileStorage>
{
    public FileValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty()
            .WithMessage("File id cannot be empty");
    }
}