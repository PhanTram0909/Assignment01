using Microsoft.Extensions.Configuration;
using System.IO;

namespace PhanNgocBaoTramWPF.Helpers
{
    public static class ConfigHelper
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }
    }
}
