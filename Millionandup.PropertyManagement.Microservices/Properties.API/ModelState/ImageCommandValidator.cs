using FluentValidation;
using Properties.Application.Commands;
using Properties.Application.Dto;

namespace Properties.API.ModelState;

public class ImageCommandValidator : AbstractValidator<CreateImageCommand>
{
    public ImageCommandValidator()
    {
        RuleFor(o => o.InernalCode).NotEmpty().MaximumLength(30).MinimumLength(4);
        RuleFor(o => o.File).NotEmpty().Must(ValidateImage);
    }
    private bool ValidateImage(string img)
    {
        Span<byte> buffer = new(new byte[img.Length]);
        return (Convert.TryFromBase64String(img, buffer, out _));
    }
}
