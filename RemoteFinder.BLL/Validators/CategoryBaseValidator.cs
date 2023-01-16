using FluentValidation;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Validators;

public class CategoryBaseValidator : AbstractValidator<Category>
{
    public CategoryBaseValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .WithMessage("Id cannot be empty");
    }
}