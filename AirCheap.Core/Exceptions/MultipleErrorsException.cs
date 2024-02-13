namespace AirCheap.Core.Exceptions;

public class MultipleErrorsException : Exception
{
    public List<string> Errors { get; }

    public MultipleErrorsException(List<string> errors)
    {
        Errors = errors;
    }
}