using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

namespace CoryCharlton.LoggingBenchmarks.Logging
{
    /// <summary>
    /// The provider for the <see cref="NullLogger"/>.
    /// </summary>
    [ProviderAlias("Null")]
    internal class NullLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly ConcurrentDictionary<string, NullLogger> _loggers = new();

        private IExternalScopeProvider _scopeProvider = NullExternalScopeProvider.Instance;

        /// <inheritdoc />
        public ILogger CreateLogger(string name)
        {
            return _loggers.GetOrAdd(name, new NullLogger(name)
            {
                ScopeProvider = _scopeProvider,
            });
        }

        /// <inheritdoc />
        public void Dispose()
        {
        }

        /// <inheritdoc />
        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;

            foreach (var logger in _loggers)
            {
                logger.Value.ScopeProvider = _scopeProvider;
            }
        }
    }
}
