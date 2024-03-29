using FluentValidation;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Validators;

public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(c => c.Id)
            .NotNull()
            .WithMessage("Id cannot be empty");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty");
    }
}