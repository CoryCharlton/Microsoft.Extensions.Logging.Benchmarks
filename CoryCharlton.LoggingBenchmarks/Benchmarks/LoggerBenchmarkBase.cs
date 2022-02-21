using BenchmarkDotNet.Attributes;
using CoryCharlton.LoggingBenchmarks.Logging;
using Microsoft.Extensions.Logging;

// ReSharper disable InconsistentNaming
namespace CoryCharlton.LoggingBenchmarks.Benchmarks
{
    [MemoryDiagnoser]
    public abstract partial class LoggerBenchmarkBase
    {
        protected const string Message = "Log message with {Parameter1}, {Parameter2}, {Parameter3}, {Parameter4}, {Parameter5}, {Parameter6} parameters.";

        protected readonly ILogger<LoggerBenchmarkBase> _logger;
      
        protected readonly Action<ILogger, Guid, DateTime, int, double, long, float, Exception?> _logEnabled =
            LoggerMessage.Define<Guid, DateTime, int, double, long, float>(
                logLevel: LogLevel.Information,
                eventId: 1,
                formatString: Message);

        protected readonly Action<ILogger, Guid, DateTime, int, double, long, float, Exception?> _logDisabled =
            LoggerMessage.Define<Guid, DateTime, int, double, long, float>(
                logLevel: LogLevel.Debug,
                eventId: 2,
                formatString: Message);


        [LoggerMessage(EventId = 3, Level = LogLevel.Information, Message = Message)]
        protected partial void LogEnabled(Guid parameter1, DateTime parameter2, int parameter3, double parameter4, long parameter5, float parameter6);

        [LoggerMessage(EventId = 4, Level = LogLevel.Debug, Message = Message)]
        protected partial void LogDisabled(Guid parameter1, DateTime parameter2, int parameter3, double parameter4, long parameter5, float parameter6);

        [LoggerMessage(EventId = 5, Message = Message)]
        protected partial void LogDynamicLevel(LogLevel level, Guid parameter1, DateTime parameter2, int parameter3, double parameter4, long parameter5, float parameter6);

        protected LoggerBenchmarkBase()
        {
            var loggerFactory = LoggerFactory.Create(builder => builder.AddTarget(Target));
            _logger = loggerFactory.CreateLogger<LoggerBenchmarkBase>();
        }

        protected LoggerTarget Target { get; set; } = LoggerTarget.Console;

        // There values do not change between executions of a given benchmark
        protected Guid Parameter1 { get; } = Guid.NewGuid();
        protected DateTime Parameter2 { get; } = DateTime.UtcNow;
        protected int Parameter3 { get; } = Random.Shared.Next();
        protected double Parameter4 { get; } = Random.Shared.NextDouble();
        protected long Parameter5 { get; } = Random.Shared.NextInt64();
        protected float Parameter6 { get; } = Random.Shared.NextSingle();
    }
}
