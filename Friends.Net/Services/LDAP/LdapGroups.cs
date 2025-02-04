using Friends.Net.Services.LDAP.Models;
using LdapForNet;
using static LdapForNet.Native.Native;

namespace Friends.Net.Services.LDAP
{
    public class LdapGroups(LdapConnection _connection, LdapConfig _config)
    {
        public async Task<LdapGroupDto[]> GetAll()
        {
            List<LdapGroupDto> ldapGroups = new();

            var directoryRequest = new SearchRequest($"{_config.LdapGroupPathDn},{_config.LdapBaseDn}", "(objectclass=groupOfNames)", LdapSearchScope.LDAP_SCOPE_SUB);
            directoryRequest.Controls.Add(new SortRequestControl("cn", true));
            //directoryRequest.Attributes.AddRange(["uid", "givenName", "sn", "cn", "displayName", "mail", "nsaccountlock"]);
            var response = (SearchResponse)_connection.SendRequest(directoryRequest);
            foreach (var entry in response.Entries)
            {
                string[] members = [];
                if (entry.Attributes.Contains("member"))
                {
                    var member = entry.Attributes["member"]?.GetValues<string>();
                    members = member?.Select(m => m.ToString()).ToArray() ?? [];
                }
                var objClass = entry.Attributes["objectClass"]?.GetValues<string>();
                var cn = entry.Attributes["cn"]?.GetValues<string>();
                var description = entry.Attributes["description"]?.GetValues<string>();
                var ipaUniqueID = entry.Attributes["ipaUniqueID"]?.GetValues<string>();
                ldapGroups.Add(new LdapGroupDto
                {
                    ObjectClass = objClass?.FirstOrDefault() ?? "",
                    Cn = cn?.FirstOrDefault() ?? "",
                    Description = description?.FirstOrDefault() ?? "",
                    Uid = ipaUniqueID?.FirstOrDefault() ?? "",
                    Members = members
                });
            }
            return await Task.FromResult(ldapGroups.ToArray());
        }
    }
}
