using Microsoft.AspNetCore.Identity;

namespace BeautySalon.Model.User;

public class User : IdentityUser<long>
{
    public User() : base() { }

    public string Name { get; set; }

    public string Profile { get; set; }

    public bool Active { get; set; }
    
    public DateTime DateCreated { get; set; }
}
