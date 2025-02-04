using Friends.Net.Services.LDAP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Friends.Net.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<ApplicationGroup> Groups { get; set; }
    public DbSet<ApplicationShortcut> AppShortcuts { get; set; }
    public DbSet<ApplicationImage> AppImages { get; set; }

    public bool UpdateLocalUser(LdapUserDto? updateData)
    {
        if (updateData == null)
        {
            return false;
        }
        var user = Users.FirstOrDefault(u => u.Email == updateData.Uid);
        if (user == null)
        {
            return false;
        }
        user.Email = updateData.Email;
        user.PreferredName = updateData.DisplayName;
        user.ConnectedToLdap = updateData.IsLdapObject;
        return true;
    }
}
