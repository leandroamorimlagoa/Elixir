using Microsoft.Extensions.Configuration;


namespace Elixir.App.Settings
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public bool IncludeElixirWithNoIngredients { get; set; }

        public static AppSettings? LoadAppSettings()
        {
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            return config.Get<AppSettings>();
        }
    }
}
