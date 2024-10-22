using Microsoft.Extensions.Configuration;

namespace CustomerAPI.Helpers
{
    public static class ConfigurationExtensions
    {
        public static string GetConnectionStringWithDataDirectory(this IConfiguration configuration, string name)
        {
            var connectionString = configuration.GetConnectionString(name);
            var dataDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
            return connectionString.Replace("[DataDirectory]", dataDirectoryPath);
        }
    }
}
