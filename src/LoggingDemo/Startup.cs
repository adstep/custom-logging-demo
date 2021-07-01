using LoggingDemo;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: WebJobsStartup(typeof(Startup))]
namespace LoggingDemo
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            // Add the logging pipelines to use. We are adding ApplicationInsights only.
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddApplicationInsights();
            });

            builder.Services.AddSingleton<CustomLogger>();
        }
    }
}
