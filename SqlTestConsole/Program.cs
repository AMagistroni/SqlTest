using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using SqlTestConsole.NotificationError;
using SqlTestConsole.Provider;

namespace SqlTestConsole
{
    public class Program
    {
        private static void Main()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<Program>()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
                .WriteTo.File("Log\\log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            ISqlSourceProvider sqlSourceProvider = null;
            INotificationError notificationError = null;
            if (config.GetSection(DirectoryProviderOptions.ConfigName).Exists())
            {
                sqlSourceProvider = DirectoryProvider.Create(config);
            }

            if (config.GetSection(FileNotificationErrorOptions.ConfigName).Exists())
            {
                notificationError = FileNotificationError.Create(config);
            }

            SqlTestConsoleManager manager = new(sqlSourceProvider, notificationError, config.GetConnectionString("ConnectionString"), Log.Logger);
            manager.TestQuery();
        }
    }
}
