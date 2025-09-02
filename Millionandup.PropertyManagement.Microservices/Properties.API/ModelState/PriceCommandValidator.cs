using FluentValidation;
using Properties.Application.Commands;

namespace Properties.API.ModelState;

public class PriceCommandValidator : AbstractValidator<UpdatePropertyPriceCommand>
{
    public PriceCommandValidator()
    {
        RuleFor(o => o.CodeInternal).NotEmpty();
        RuleFor(o => o.Price).NotEmpty().NotEqual(0);
    }
}
