using BenchmarkDotNet.Attributes;
using CoryCharlton.LoggingBenchmarks.Logging;
using Microsoft.Extensions.Logging;

namespace CoryCharlton.LoggingBenchmarks.Benchmarks
{
    /// <summary>
    /// A benchmark to compare a simple generic extension method vs. the <see cref="LoggerMessage.Define"/> alternatives when the log message is logged
    /// </summary>
    public class LoggerLogBenchmark : LoggerBenchmarkBase
    {
        [ParamsAllValues]
        public bool MessageLogged { get; set; }

        [Benchmark(Baseline = true)]
        public void DirectCall()
        {
            if (MessageLogged)
            {
                _logger.LogInformation(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
            }
            else
            {
                _logger.LogDebug(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
            }
        }

        [Benchmark]
        public void Interpolated()
        {
            if (MessageLogged)
            {
                _logger.LogInformation($"Log message with {Parameter1}, {Parameter2}, {Parameter3}, {Parameter4}, {Parameter5}, {Parameter6} parameters.");
            }
            else
            {
                _logger.LogDebug($"Log message with {Parameter1}, {Parameter2}, {Parameter3}, {Parameter4}, {Parameter5}, {Parameter6} parameters.");
            }
        }

        [Benchmark]
        public void IsEnabledCheck()
        {
            if (MessageLogged)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
                }
            }
            else
            {
                if (_logger.IsEnabled(LogLevel.Debug))
                {
                    _logger.LogDebug(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
                }
            }
        }

        [Benchmark]
        public void IsEnabledExtension()
        {
            if (MessageLogged)
            {
                _logger.Information(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
            }
            else
            {
                _logger.Debug(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
            }
        }

        [Benchmark]
        public void LoggerMessageDefine()
        {
            if (MessageLogged)
            {
                _logEnabled(_logger, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6, null);
            }
            else
            {
                _logDisabled(_logger, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6, null);
            }
        }

        [Benchmark]
        public void SourceGenerator()
        {
            if (MessageLogged)
            {
                LogEnabled(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
            }
            else
            {
                LogDisabled(Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
            }
        }

        [Benchmark]
        public void SourceGeneratorDynamicLevel()
        {
            LogDynamicLevel(MessageLogged ? LogLevel.Information : LogLevel.Debug, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
        }
    }
}
