using Microsoft.AspNetCore.Identity;

namespace Friends.Net.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string PreferredName { get; set; } = "";
    public List<ApplicationGroup> Groups { get; set; } = [];

    public static string[] GetVisibleProperties()
    {
        return
        [
            "PreferredName", 
            "UserName", 
            "Email", 
            "EmailConfirmed", 
            "TwoFactorEnabled", 
            "LockoutEnabled", 
            "LockoutEnd",
            "AccessFailedCount"
        ];
    }
}