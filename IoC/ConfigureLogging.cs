using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IoC
{
    public static class ConfigureLogging
    {
        public static void ConfigureWebAppLogging(this IServiceCollection serviceCollection) => serviceCollection.ConfigureCommon();

        private static void ConfigureCommon(this IServiceCollection serviceCollection)
        {

            serviceCollection.AddLogging(builder =>
                builder.AddConsole());
        }
    }
}