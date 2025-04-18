namespace FunStore.ValidationExceptions;

public class ValidationException : Exception
{
    public ValidationException(string errorMessage) : base(errorMessage)
    {

    }
}