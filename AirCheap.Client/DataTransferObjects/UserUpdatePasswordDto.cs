namespace AirCheap.Client.DataTransferObjects;

public class UserUpdatePasswordDto
{
    public string Username { get; set; }

    public string CurrentPassword { get; set; }

    public string NewPassword { get; set; }

    public string ConfirmPassword { get; set; }
}
