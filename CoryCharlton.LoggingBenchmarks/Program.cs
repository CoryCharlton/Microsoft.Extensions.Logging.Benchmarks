using BenchmarkDotNet.Running;
using CoryCharlton.LoggingBenchmarks.Benchmarks;

namespace CoryCharlton.LoggingBenchmarks;

internal class Program
{
    public static void Main(string[] args)
    {
        BenchmarkRunner.Run<LoggerLogBenchmark>();
        // BenchmarkRunner.Run<LogCallSkippedBenchmark>();
        // BenchmarkRunner.Run<LoggerMessageDefineLoggedBenchmark>();
        // BenchmarkRunner.Run<LoggerMessageDefineSkippedBenchmark>();

        //BenchmarkRunner.Run(typeof(LoggerBenchmarkBase).Assembly);
        Console.ReadKey();
    }
}

