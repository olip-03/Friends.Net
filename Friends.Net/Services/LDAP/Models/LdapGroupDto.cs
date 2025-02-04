namespace Friends.Net.Services.LDAP.Models
{
    public class LdapGroupDto : LdapObject
    {
        public string ObjectClass { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[] Members { get; set; } = [];
    }
}
