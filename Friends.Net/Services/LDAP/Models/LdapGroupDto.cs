namespace Friends.Net.Services.LDAP.Models
{
    public class LdapGroupDto
    {
        public string ObjectClass { get; set; } = string.Empty;
        public string Cn { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string[] Member { get; set; } = [];
        public string IpaUniqueID { get; set; } = string.Empty;
    }
}
