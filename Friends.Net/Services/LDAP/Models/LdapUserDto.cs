using System.Net.NetworkInformation;
using LdapForNet;
namespace Friends.Net.Services.LDAP.Models;

public class LdapUserDto
{
    public string Uid { get; set; } = string.Empty;
    public string GivenName { get; set; } = string.Empty;
    public string Sn { get; set; } = string.Empty;
    public string Cn { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string TelephoneNumber { get; set; } = string.Empty;
    public bool IsLdapUser { get; set; } = true;
    public bool IsEnable { get; set; } = true;

    public static LdapUserDto? Builder(DirectoryEntry? entry)
    {
        if (entry == null) return null;
        string? uid = entry.Attributes.TryGetValue("uid", out DirectoryAttribute? uidAttribute) ? uidAttribute?.GetValues<string>().FirstOrDefault() : "";
        string? givenName = entry.Attributes.TryGetValue("givenName", out var givenNameAttribute) ? givenNameAttribute?.GetValues<string>().FirstOrDefault() : "";
        string? sn = entry.Attributes.TryGetValue("sn", out var snAttribute) ? snAttribute?.GetValues<string>().FirstOrDefault() : "";
        string? cn = entry.Attributes.TryGetValue("sn", out var cnAttribute) ? cnAttribute?.GetValues<string>().FirstOrDefault() : "";
        string? displayName = entry.Attributes.TryGetValue("sn", out var displaynameAttribute) ? displaynameAttribute?.GetValues<string>().FirstOrDefault() : "";
        string? email = entry.Attributes.TryGetValue("sn", out var emailAttribute) ? emailAttribute?.GetValues<string>().FirstOrDefault() : "";
        string? telephone = entry.Attributes.TryGetValue("sn", out var telephoneAttribute) ? telephoneAttribute?.GetValues<string>().FirstOrDefault() : "";
        bool isEnabled = true;
        if (entry.Attributes.TryGetValue("nsaccountlock", out var nsAccountLockAttribute))
        {
            string? nsalock = nsAccountLockAttribute?.GetValues<string>().FirstOrDefault();
            isEnabled = !(nsalock?.ToLower() == "true");
        }

        return new LdapUserDto 
        {
            Uid = uid ?? string.Empty,
            GivenName = givenName ?? string.Empty,
            Sn = sn ?? string.Empty,
            Cn = cn ?? string.Empty,
            DisplayName = displayName ?? string.Empty,
            Email = email ?? string.Empty,
            TelephoneNumber = telephone ?? string.Empty,
            IsEnable = isEnabled
        };
    }
}