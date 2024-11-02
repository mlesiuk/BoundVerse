namespace BoundVerse.Application.Exceptions;

public sealed class InvalidInputException : Exception
{
    public InvalidInputException() : base("Invalid input.") { }
    public InvalidInputException(string message) : base(message) { }
}
