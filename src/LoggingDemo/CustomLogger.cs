using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace LoggingDemo
{
    public class CustomLogger
    {
        private readonly IExternalScopeProvider _scopeProvider;
        private readonly ILogger<CustomLogger> _logger;

        public CustomLogger(IExternalScopeProvider scopeProvider, ILogger<CustomLogger> logger)
        {
            _scopeProvider = scopeProvider;
            _logger = logger;
        }

        public void Log()
        {
            var currentActivity = Activity.Current;

            if (currentActivity == null)
            {
                _logger.LogInformation("Activity.Current not defined");
            }

            var scopeProps = GetScopeDictionaryOrNull(_scopeProvider);

            if (scopeProps == null)
            {
                _logger.LogInformation("IExternalScopeProvider not defined");
            }
        }

        private static IDictionary<string, object> GetScopeDictionaryOrNull(IExternalScopeProvider scopeProvider)
        {
            IDictionary<string, object> result = null;

            if (scopeProvider == null)
                return result;

            scopeProvider.ForEachScope((scope, _) =>
            {
                if (scope is IEnumerable<KeyValuePair<string, object>> kvps)
                {
                    result ??= new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

                    foreach (var (key, value) in kvps)
                    {
                        result[key] = value;
                    }
                }
            }, (object)null);

            return result;
        }
    }
}
