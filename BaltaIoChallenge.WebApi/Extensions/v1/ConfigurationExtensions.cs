using System.Reflection;

namespace BaltaIoChallenge.WebApi.Extensions.v1
{
    public static class ConfigurationExtensions
    {
        public static void LoadConfigurationValues(this IConfiguration configuration)
        {
            foreach (var property in typeof(Configuration).GetTypeInfo().GetFields().Where(x => x.IsStatic))
            {
                try
                {
                    var value = configuration[$"{property.Name}"];

                    property.SetValue(null, value);
                }
                catch
                {
                    throw new Exception("Something went wrong while trying to configurate the environment.");
                }
            }
        }
    }
}
