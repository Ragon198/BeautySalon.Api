namespace BeautySalon.Crosscutting.Exceptions;

public class BeautySalonApplicationException : ApplicationException
{
    public BeautySalonApplicationException() { }
    
    public BeautySalonApplicationException(string message) : base(message) { }

    public BeautySalonApplicationException(string message, Exception exception) : base(message, exception) { }
}

public class BeautySalonNotFoundException : Exception
{
    public BeautySalonNotFoundException() { }
    
    public BeautySalonNotFoundException(string message) : base(message) { }

    public BeautySalonNotFoundException(string message, Exception exception) : base(message, exception) { }
}
