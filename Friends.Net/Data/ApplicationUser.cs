using Friends.Net.Services.LDAP.Models;
using Microsoft.AspNetCore.Identity;
using System.Xml.Linq;

namespace Friends.Net.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string PreferredName { get; set; } = "";
    public bool ConnectedToLdap { get; set; } = false;
    public bool IsEmpty()
    {
        return
        string.IsNullOrWhiteSpace(PreferredName) &&
        string.IsNullOrWhiteSpace(UserName) && 
        string.IsNullOrWhiteSpace(Email);
    }
    public LdapUserDto ToLdap()
    {
        return new LdapUserDto
        {
            Sn = UserName,
            Email = Email,
            DisplayName = PreferredName,
            IsLdapUser = false
        };
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