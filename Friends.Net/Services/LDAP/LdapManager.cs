using Friends.Net.Data;
using Friends.Net.Services.LDAP.Models;
using LdapForNet;
using LdapForNet.Native;

namespace Friends.Net.Services.LDAP
{
    public class LdapManager
    {
        public LdapUsers Users { get; private set; }
        public LdapGroups Group { get; private set; }
        
        private LdapConnection _connection = new LdapConnection();

        private readonly LdapConfig _config;

        // Constructor for dependency injection
        public LdapManager(string ldapPath, string ldapUsername, string ldapPassword)
        {
            _config = new LdapConfig()
            {
                LdapPath = ldapPath,
                LdapUsername = ldapUsername,
                LdapPassword = ldapPassword
            };
        }
        public LdapManager(LdapConfig? config)
        {
            ArgumentNullException.ThrowIfNull(config);
            _config = config;
        }

        public async Task<bool> ConnectAsync()
        {
            string connectionString = $"uid={_config.LdapUsername},{_config.LdapAccountPathDn},{_config.LdapBaseDn}";
            _connection.Connect(new Uri(_config.LdapPath));
            // _connection.StartTransportLayerSecurity(true); 
            _connection.TrustAllCertificates();
            await _connection.BindAsync(Native.LdapAuthType.Simple, new LdapCredential
            {
                UserName = connectionString,
                Password = _config.LdapPassword
            });
            
            Users = new LdapUsers(_connection, _config);
            Group = new LdapGroups(_connection, _config);

            return true;
        }

        public string GetPath()
        {
            return _config.LdapPath;
        }
    }
}
