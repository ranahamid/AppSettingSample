using AppSettingSample.Models;
using Microsoft.Extensions.Options;

namespace AppSettingSample
{
    public static  class ConfigurationExtension
    {
        public static void  AddConfiguration<T>(this IServiceCollection service, IConfiguration configuration, string sectionName) where T : class
        {
            if (sectionName == null)
            {
                sectionName = typeof(T).Name;
            }

            var instance = Activator.CreateInstance<T>();
            new ConfigureFromConfigurationOptions<T>(configuration.GetSection(sectionName)).Configure(instance);
            service.AddSingleton(instance);
        }
    }
}
