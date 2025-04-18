namespace FunStore.ValidationExceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User with provided username is not found")
    {
        
    }
}