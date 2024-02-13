using Microsoft.AspNetCore.Identity;

namespace AirCheap.DAL.Entities;

public class UserEntity : IdentityUser
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool IsDeleted { get; set; }
}