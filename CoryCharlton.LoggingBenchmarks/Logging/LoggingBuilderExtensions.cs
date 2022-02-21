using Microsoft.Extensions.Logging;

namespace CoryCharlton.LoggingBenchmarks.Logging
{
    internal static class LoggingBuilderExtensions
    {
        public static ILoggingBuilder AddTarget(this ILoggingBuilder builder, LoggerTarget target)
        {
            return target switch
            {
                LoggerTarget.Console => builder.AddJsonConsole(options => options.IncludeScopes = true),
                //LoggerTarget.Debug => builder.AddDebug(),
                LoggerTarget.Null => builder.AddNull(),
                _ => throw new NotImplementedException($"The {target} target is not currently implemented.")
            };
        }
    }
}
