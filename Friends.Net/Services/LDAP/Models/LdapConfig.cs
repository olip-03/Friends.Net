namespace Friends.Net.Services.LDAP.Models
{
    public class LdapConfig
    {
        public string LdapPath { get; set; } = string.Empty;
        public string LdapBaseDn { get; set; } = string.Empty;
        public string LdapAccountPathDn { get; set; } = string.Empty;
        public string LdapGroupPathDn { get; set; } = string.Empty;
        public string LdapUsername { get; set; } = string.Empty;
        public string LdapPassword { get; set; } = string.Empty;
    }

}
