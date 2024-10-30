namespace BoundVerse.Application.Exceptions;

public sealed class NotFoundException(string entityName) : Exception($"'{entityName}' not found.") { }
