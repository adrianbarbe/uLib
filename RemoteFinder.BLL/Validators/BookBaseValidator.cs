using FluentValidation;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Validators;

public class BookBaseValidator : AbstractValidator<BookBase>
{
    public BookBaseValidator()
    {
        RuleFor(b => b.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty");

        RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");

        RuleFor(b => b.File)
            .NotEmpty()
            .WithMessage("File cannot be empty")
            .SetValidator(new FileValidator());
    }
}