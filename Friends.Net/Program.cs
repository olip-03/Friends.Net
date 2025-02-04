using System.Diagnostics;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Friends.Net.Components;
using Friends.Net.Components.Account;
using Friends.Net.Data;
using Friends.Net.Services;
using Friends.Net.Services;
using Friends.Net.Services.LDAP;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using MudBlazor.Services;
using Friends.Net.Components.Pages.SettingsPages;
using Friends.Net.Services.LDAP.Models;

namespace Friends.Net
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddBlazorBootstrap();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddScoped<IdentityUserAccessor>();
            builder.Services.AddScoped<IdentityRedirectManager>();
            builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

            builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<ApplicationGroup>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();


            builder.Services.AddMudServices();

            // Add LdapManager as a singleton service
            builder.Services.Configure<LdapConfig>(builder.Configuration.GetSection("LdapConfig"));
            
            
            builder.Services.AddSingleton(sp =>
                new LdapManager(builder.Configuration.GetSection("LdapConfig").Get<LdapConfig>()));
            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultScheme = IdentityConstants.ApplicationScheme;
                    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
                })
                .AddIdentityCookies();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                                   throw new InvalidOperationException(
                                       "Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error", createScopeForErrors: true);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAntiforgery();

            app.MapStaticAssets();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // Add additional endpoints required by the Identity /Account Razor components.
            app.MapAdditionalIdentityEndpoints();

            var seedRoles = InitializationService.SeedRoles(app.Services);
            seedRoles.Wait();
            if (!seedRoles.Result)
            {
                Trace.WriteLine("Did not seed roles");
            }
            
            // 3. Create a scope to resolve and call your LDAPManager
            using (var scope = app.Services.CreateScope())
            {
                var ldapManager = scope.ServiceProvider.GetRequiredService<LdapManager>();
                bool connected = await ldapManager.ConnectAsync();  // Make your connection attempt here
            }

            app.Run();
        }
    }
}