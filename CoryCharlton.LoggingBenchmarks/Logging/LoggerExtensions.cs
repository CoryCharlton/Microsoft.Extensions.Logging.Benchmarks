using Microsoft.Extensions.Logging;

namespace CoryCharlton.LoggingBenchmarks.Logging
{
    /// <summary>
    /// Provider generic log extensions that check <see cref="ILogger{TCategoryName}.IsEnabled"/> to avoid boxing if the message won't be logged.
    /// Normally I'd have overrides for all <see cref="LogLevel"/> and 0 to 6 parameters but I'm only copying over what I need for the benchmarks
    /// </summary>
    internal static class LoggerExtensions
    {
        public static void Debug<T1, T2, T3, T4, T5, T6>(this ILogger logger, string message, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                logger.LogDebug(message, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            }
        }

        public static void Information<T1, T2, T3, T4, T5, T6>(this ILogger logger, string message, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation(message, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
            }
        }
    }
}
