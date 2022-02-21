using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace CoryCharlton.LoggingBenchmarks.Logging
{
    /// <summary>
    /// Extension methods for the <see cref="ILoggerFactory"/> class.
    /// </summary>
    public static class NullLoggerFactoryExtensions
    {
        /// <summary>
        /// Adds a <see cref="NullLogger"/> named 'Null' to the factory.
        /// </summary>
        /// <param name="builder">The extension method argument.</param>
        public static ILoggingBuilder AddNull(this ILoggingBuilder builder)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, NullLoggerProvider>());

            return builder;
        }
    }
}
