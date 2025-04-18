namespace FunStore.Models.Request;

public class RegisterRequestModel
{
    public string Username { get; set; }
    public string Password { get; set; }

    // Customer Info
    public string FirstName { get; set; }
    public string LastName { get; set; }
}