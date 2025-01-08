using System.Diagnostics;
using Friends.Net.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Friends.Net.Services;

public static class InitializationService
{
    private static readonly string[] Roles = new string[] {"Admin" };
    public static async Task<bool> SeedRoles(IServiceProvider serviceProvider)
    {
        using var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
        if (dbContext == null) return false;
        
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<ApplicationGroup>>();
        var groups = roleManager.Roles.Select(r => r.Name).ToList();
        
        foreach (var role in Roles)
        {
            if (!groups.Contains(role))
            {
                Trace.WriteLine($"InitializationService: Seeded Role {role}.");
                await roleManager.CreateAsync(new ApplicationGroup(role));
            }
        }
        return true;
    }
}