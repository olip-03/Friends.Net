using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace Friends.Net.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string PreferredName { get; set; } = "";
    public bool IsEmpty()
    {
        return
        string.IsNullOrWhiteSpace(PreferredName) &&
        string.IsNullOrWhiteSpace(UserName) && 
        string.IsNullOrWhiteSpace(Email);
    }
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