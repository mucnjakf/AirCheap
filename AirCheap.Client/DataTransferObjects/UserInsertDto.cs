﻿namespace AirCheap.Client.DataTransferObjects;

public class UserInsertDto
{
    public string Username { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}