namespace Friends.Net.Services.LDAP.Models
{
    public class LdapObject
    {
        public string Uid { get; set; } = string.Empty;
        public string Cn { get; set; } = string.Empty;
        public bool IsLdapObject { get; set; } = true;
    }
}
