using Microsoft.Extensions.Logging;

namespace CoryCharlton.LoggingBenchmarks.Logging
{
    /// <summary>
    /// An <see cref="ILogger"/> implementation that copies all the same logic from <see cref="Microsoft.Extensions.Logging.Debug.DebugLogger"/> but does not actually log
    /// </summary>
    internal sealed class NullLogger: ILogger
    {
        private long _count;
        private readonly string _name;

        public NullLogger(string name)
        {
            
            _name = name;
        }

        internal IExternalScopeProvider? ScopeProvider { get; set; }

        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state) => ScopeProvider?.Push(state) ?? NullScope.Instance;

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            message = $"{ logLevel }: {message}";

            if (exception != null)
            {
                message += Environment.NewLine + Environment.NewLine + exception;
            }

            _count++;
        }
    }
}
