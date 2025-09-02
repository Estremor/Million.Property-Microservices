namespace Properties.Application.Commands;

public record CreateImageCommand(string File, string InernalCode, bool Enabled) : ICommand;
