#pragma warning disable CS0612 // Type or member is obsolete

using Friends.Net.Services.LDAP.Models;
using LdapForNet;
using LdapForNet.Native;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static LdapForNet.Native.Native;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Friends.Net.Services.LDAP;

public class LdapUsers(LdapConnection _connection, LdapConfig _config)
{
    public async Task<SignInResult> AuthenticateUser(string username, string password)
    {
        try
        {
            string connectionString = $"uid={username.Split('@').First()},{_config.LdapAccountPathDn},{_config.LdapBaseDn}";
            _connection.Connect(new Uri(_config.LdapPath));
            // _connection.StartTransportLayerSecurity(true); 
            _connection.TrustAllCertificates();
            await _connection.BindAsync(Native.LdapAuthType.Simple, new LdapCredential
            {
                UserName = connectionString,
                Password = password
            });
            LdapUserDto? user = await Get(username.Split('@').First());
            if (user == null)
            {
                return SignInResult.Failed;
            }
            else if(!user.IsEnable)
            {
                return SignInResult.LockedOut;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return SignInResult.Failed;
        }
        return SignInResult.Success;
    }

    public async Task<LdapUserDto[]> GetAll()
    {
        List<LdapUserDto> ldapUsers = new();
        await Task.Run(() =>
        {
            var directoryRequest = new SearchRequest($"{_config.LdapAccountPathDn},{_config.LdapBaseDn}", "(objectclass=ipaobject)", LdapSearchScope.LDAP_SCOPE_SUB);
            directoryRequest.Controls.Add(new SortRequestControl("cn", true));
            directoryRequest.Attributes.AddRange(["uid", "givenName", "sn", "cn", "displayName", "mail", "nsaccountlock"]);
            var response = (SearchResponse)_connection.SendRequest(directoryRequest);
            foreach (var entry in response.Entries)
            {
                var user = LdapUserDto.Builder(entry);
                if (user != null)
                {
                    ldapUsers.Add(user);
                }
            }
        });
        return ldapUsers.ToArray();
    }

    public async Task<LdapUserDto?> Get(string uid)
    {
        if (string.IsNullOrWhiteSpace(uid))
        {
            throw new ArgumentException("UID cannot be null or empty", nameof(uid));
        }
        DirectoryEntry? user = null;
        await Task.Run(() =>
        {
            SearchRequest request = new SearchRequest(
                $"{_config.LdapAccountPathDn},{_config.LdapBaseDn}",
                $"uid={uid}",
                LdapSearchScope.LDAP_SCOPE_SUBTREE,
                ["uid", "givenName", "sn", "cn", "objectClass", "displayName", "mail", "nsaccountlock"]);
            var response = (SearchResponse)_connection.SendRequest(request);
            user = response.Entries.FirstOrDefault();
        });
        if (user == null)
        {
            return null; // User not found
        }
        return LdapUserDto.Builder(user);
    }

    public async Task<bool> Create(LdapUserDto toCreate)
    {
        string dn = $"{_config.LdapAccountPathDn},{_config.LdapBaseDn}";
        await _connection.AddAsync(new LdapEntry
        {
            Dn = dn,
            Attributes = new Dictionary<string, List<string>>
            {
                {"sn", [toCreate.Sn] },
                {"objectclass", ["inetOrgPerson"] },
                {"displayName", [toCreate.DisplayName] },
                {"description", ["your_description"] },
                {"password", [toCreate.Password] }
            }
        });
        return true;
    }
}
#pragma warning restore CS0612 // Type or member is obsolete