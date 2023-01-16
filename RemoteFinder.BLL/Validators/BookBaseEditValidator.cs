using FluentValidation;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Validators;

public class BookBaseEditValidator : AbstractValidator<BookBase>
{
    public BookBaseEditValidator()
    {
        RuleFor(b => b.Id)
            .NotNull()
            .WithMessage("Id cannot be empty");

        RuleFor(b => b.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
    }
}