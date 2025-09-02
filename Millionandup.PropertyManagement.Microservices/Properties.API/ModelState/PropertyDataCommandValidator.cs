using FluentValidation;
using Properties.Application.Commands;
using Properties.Application.Dto;

namespace Properties.API.ModelState;

public class PropertyDataCommandValidator : AbstractValidator<CreatePropertyCommand>
{
    public PropertyDataCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(150);
        RuleFor(p => p.OwnerDocument).NotEmpty().MinimumLength(4);
        RuleFor(p => p.Address).NotEmpty().MaximumLength(200);
        RuleFor(p => p.CodeInternal).NotEmpty().MaximumLength(300);
        RuleFor(p => p.Price).NotEmpty().NotEqual(0);
        RuleFor(p => p.Year).NotEmpty().NotEqual(0).Must(ValidateYear);
        RuleFor(p => p.PropertyImages).Must(ValidateImages);
    }

    public bool ValidateYear(int year)
    {
        bool result = DateTime.TryParse(string.Format("1/1/{0}", year), out _);
        return result;
    }

    private bool ValidateImages(List<Image> imgs)
    {
        bool result = true;
        if (imgs is not null && imgs.Count > 0)
        {
            foreach (var img in imgs)
            {
                Span<byte> buffer = new(new byte[img.File.Length]);
                if (!Convert.TryFromBase64String(img.File, buffer, out _))
                {
                    result = false;
                    break;
                }
            }

        }
        return result;
    }
}
