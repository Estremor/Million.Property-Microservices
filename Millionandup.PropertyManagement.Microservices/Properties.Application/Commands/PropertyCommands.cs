using Properties.Application.Dto;
using MediatR;

namespace Properties.Application.Commands;

public interface ICommand : IRequest { }
public record CreatePropertyCommand(string Name, string Address, decimal Price, string CodeInternal, int Year, string OwnerDocument, List<Image> PropertyImages) : ICommand;
/// <summary>
/// Información del seguimiento de la propiedad.
/// </summary>
public record UpdatePropertyCommand(string CodeInternal, string Name, string Address, decimal Price, int Year, string OwnerDocument, decimal? Value, decimal? Tax, string DataSale): ICommand;
public record UpdatePropertyPriceCommand(string CodeInternal, decimal Price) : ICommand;