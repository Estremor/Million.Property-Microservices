namespace Properties.Application.Commands;


/// <summary>
/// Datos del comprador o propietario
/// </summary>
public record CreateOwnerCommand(string Name, string Address, string Document, string Photo, string Birthday) : ICommand;
