using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

public static class LoggingExtensions
{
    /// <summary>
    /// AddSerilog extension is used to register serilog logging library
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IHostBuilder AddSerilog(this IHostBuilder builder, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

        return builder.UseSerilog();
    }
}
