using Web.Infrastructure.Microservices.Shared.Logic;

namespace Web.Infrastructure.Microservices.Client.Logic
{
    public class DefaultMethodEndpointProvider : BaseMethodEndpointProvider
    {
        public DefaultMethodEndpointProvider() : base("{controller}/{action}/{id?}")
        {
        }
    }
}
