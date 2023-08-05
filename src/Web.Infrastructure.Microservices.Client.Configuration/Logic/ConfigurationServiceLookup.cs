using Microsoft.Extensions.Configuration;
using Web.Infrastructure.Microservices.Client.Interfaces;

namespace Web.Infrastructure.Microservices.Client.Configuration.Logic
{
    public class ConfigurationServiceLookup : IServiceLookup
    {
        private readonly IConfiguration _configuration;
        private readonly string _microservicesParentPrefix;

        public ConfigurationServiceLookup(IConfiguration configuration, string microservicesParentPrefix)
        {
            _configuration = configuration;
            _microservicesParentPrefix = microservicesParentPrefix;
        }

        public Uri? Lookup(string serviceName)
        {
            var result = _configuration[$"{_microservicesParentPrefix}:{serviceName}"];

            if (string.IsNullOrEmpty(result))
            {
                return null;
            }

            return new Uri(result);
        }
    }
}
