using FluentValidation;
using Properties.Application.Commands;

namespace Properties.API.ModelState;

public class PropertyTraceCommandValidator : AbstractValidator<UpdatePropertyCommand>
{
    public PropertyTraceCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(150).NotNull();
        RuleFor(p => p.OwnerDocument).NotEmpty().MinimumLength(4).NotNull();
        RuleFor(p => p.Address).NotEmpty().MaximumLength(200);
        RuleFor(p => p.CodeInternal).NotEmpty().MaximumLength(300).MinimumLength(2).NotNull();
        RuleFor(p => p.Price).NotEmpty().NotEqual(0).NotNull();
        RuleFor(p => p.Year).NotEmpty().NotEqual(0).Must(ValidateYear).NotNull();
    }

    public bool ValidateYear(int year)
    {
        bool result = DateTime.TryParse(string.Format("1/1/{0}", year), out _);
        return result;
    }
}
