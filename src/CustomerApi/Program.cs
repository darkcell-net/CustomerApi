using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Formatting.Json;
using Serilog.Sinks.RollingFileAlternate;
using System.IO;

namespace CustomerApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseUrls("http://*:6000")
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(
                    (hostingContext, config) => config
                        .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true))
                .ConfigureLogging((hostingContext, logging) =>
                    logging.AddProvider(CreateLoggerProvider(hostingContext.Configuration)))
                .Build()
                .Run();
        }

        private static SerilogLoggerProvider CreateLoggerProvider(IConfiguration configuration)
        {
            LoggerConfiguration logConfig = new LoggerConfiguration()
                .WriteTo.RollingFileAlternate(new JsonFormatter(), "./logs", fileSizeLimitBytes: 10 * 1024 * 1024, retainedFileCountLimit: 10)
                .ReadFrom.Configuration(configuration);

            return new SerilogLoggerProvider(logConfig.CreateLogger());
        }
    }
}