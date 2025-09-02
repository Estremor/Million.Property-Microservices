using FluentValidation;
using Properties.Application.Commands;

namespace Properties.API.ModelState;

public class OwnerCommandValidator : AbstractValidator<CreateOwnerCommand>
{
    public OwnerCommandValidator()
    {

        RuleFor(o => o.Name).NotEmpty().MaximumLength(60).MinimumLength(3);
        RuleFor(o => o.Document).NotEmpty();
        RuleFor(o => o.Document).MaximumLength(30).MinimumLength(4);
        RuleFor(o => o.Birthday).NotEmpty().Must(ValidDate).WithMessage("Enter a valid date");
        RuleFor(o => o.Photo).Must(ValidateImage).WithMessage("Enter a valid Base64 string image");
    }

    private bool ValidDate(string date)
    {
        var isDate = DateTime.TryParse(date, out DateTime dateRes);
        if (isDate)
        {
            isDate = dateRes.Year <= DateTime.Now.Year;
        }
        return isDate;
    }

    private bool ValidateImage(string imgBase64)
    {
        if (!string.IsNullOrWhiteSpace(imgBase64))
        {
            Span<byte> buffer = new(new byte[imgBase64.Length]);
            return Convert.TryFromBase64String(imgBase64, buffer, out _);
        }
        return true;

    }
}
