namespace AirCheap.Core.Models;

public class UserUpdatePassword
{
    public string Username { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }
}
