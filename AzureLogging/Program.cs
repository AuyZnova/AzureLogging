using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Serilog;

namespace AzureLogging
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.WithProperty("ApplicationName", "AzureLogging")
                        .Enrich.WithProperty("Environment", "AuySpace")
                        .Enrich.WithProperty("Logger", "Serilog")
                        //.WriteTo.File(@"C:\logs\test\serilog.log")
                        //.WriteTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = "0f08fccc-1618-409a-83d5-b4468fa4c21b" }, TelemetryConverter.Traces)
                        //.WriteTo.File(
                        //    @"D:\home\LogFiles\Application\trace.log",
                        //    fileSizeLimitBytes: 5_000_000,
                        //    rollOnFileSizeLimit: true,
                        //    shared: true,
                        //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                );
    }
}
