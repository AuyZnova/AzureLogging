using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using Serilog;
using Serilog.Sinks.Logz.Io;

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
                        .Enrich.WithProperty("APP-NAME", "TestLogging")
                        .Enrich.WithProperty("ENV", "Test")
                        .Enrich.WithProperty("Logger", "Serilog")
                        .WriteTo.ApplicationInsights(new TelemetryConfiguration { InstrumentationKey = "b6bb24e4-9e6f-44e9-8ca4-b15ec94bae7c" }, TelemetryConverter.Traces)
                        .WriteTo.LogzIoDurableHttp("https://listener-au.logz.io:8071/?type=app&token=hhXkmkucBmcNroDVSsfTYcOkmDdhLSPE",
                            logzioTextFormatterOptions: new LogzioTextFormatterOptions
                            {
                                BoostProperties = true,
                                LowercaseLevel = true,
                                IncludeMessageTemplate = true,
                                FieldNaming = LogzIoTextFormatterFieldNaming.CamelCase,
                                EventSizeLimitBytes = 261120,
                            })
                        //.WriteTo.File(@"C:\logs\test\serilog.log")
                        //.WriteTo.File(
                        //    @"D:\home\LogFiles\Application\trace.log",
                        //    fileSizeLimitBytes: 5_000_000,
                        //    rollOnFileSizeLimit: true,
                        //    shared: true,
                        //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                );
    }
}
