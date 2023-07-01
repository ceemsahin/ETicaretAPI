using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationService(this IServiceCollection collection)
        {

            collection.AddMediatR(typeof(ServiceRegistration));
            collection.AddHttpClient();
        }



    }

}
