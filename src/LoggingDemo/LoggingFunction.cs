using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LoggingDemo
{
    public class LoggingFunction
    {
        private readonly CustomLogger _customLogger;

        public LoggingFunction(CustomLogger customLogger)
        {
            _customLogger = customLogger;
        }

        [FunctionName("Logger")]
        public void Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation("Helloworld!");

            _customLogger.Log();
        }
    }
}
