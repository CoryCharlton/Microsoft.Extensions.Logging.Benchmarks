# Microsoft.Extensions.Logging.Benchmarks

## TLDR ... Results

As always you should benchmark your own use cases and don't make the same mistakes I made below when doing so...

``` ini
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1566 (21H2)
Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.200
  [Host]     : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
  DefaultJob : .NET 6.0.2 (6.0.222.6406), X64 RyuJIT
```

### NullLogger

|                      Method | MessageLogged |         Mean |      Error |     StdDev | Ratio | RatioSD |  Gen 0 | Allocated |
|---------------------------- |-------------- |-------------:|-----------:|-----------:|------:|--------:|-------:|----------:|
|                  **DirectCall** |         **False** |   **165.485 ns** |  **3.2606 ns** |  **3.8815 ns** |  **1.00** |    **0.00** | **0.0534** |     **224 B** |
|                Interpolated |         False |   739.486 ns |  8.2987 ns |  7.3566 ns |  4.45 |    0.14 | 0.0782 |     328 B |
|              IsEnabledCheck |         False |     6.947 ns |  0.0755 ns |  0.0706 ns |  0.04 |    0.00 |      - |         - |
|          IsEnabledExtension |         False |     7.361 ns |  0.1407 ns |  0.1175 ns |  0.04 |    0.00 |      - |         - |
|         LoggerMessageDefine |         False |     8.878 ns |  0.0887 ns |  0.0786 ns |  0.05 |    0.00 |      - |         - |
|             SourceGenerator |         False |     6.918 ns |  0.0787 ns |  0.0736 ns |  0.04 |    0.00 |      - |         - |
| SourceGeneratorDynamicLevel |         False |     8.363 ns |  0.0783 ns |  0.0612 ns |  0.05 |    0.00 |      - |         - |
|                             |               |              |            |            |       |         |        |           |
|                  **DirectCall** |          **True** | **1,235.718 ns** | **12.9013 ns** | **11.4367 ns** |  **1.00** |    **0.00** | **0.2193** |     **920 B** |
|                Interpolated |          True |   883.763 ns | 15.1336 ns | 14.1560 ns |  0.72 |    0.01 | 0.1678 |     704 B |
|              IsEnabledCheck |          True | 1,248.736 ns | 17.9719 ns | 15.0074 ns |  1.01 |    0.02 | 0.2213 |     928 B |
|          IsEnabledExtension |          True | 1,243.679 ns | 10.7653 ns | 10.0699 ns |  1.01 |    0.01 | 0.2213 |     928 B |
|         LoggerMessageDefine |          True | 1,238.072 ns | 24.4076 ns | 22.8309 ns |  1.00 |    0.02 | 0.2213 |     928 B |
|             SourceGenerator |          True | 1,221.572 ns | 11.0601 ns |  9.2357 ns |  0.99 |    0.01 | 0.2193 |     920 B |
| SourceGeneratorDynamicLevel |          True |   948.318 ns | 13.6372 ns | 12.0890 ns |  0.77 |    0.01 | 0.1850 |     776 B |

### ConsoleLogger

|                      Method | MessageLogged |           Mean |          Error |         StdDev |         Median | Ratio | RatioSD |  Gen 0 |  Gen 1 | Allocated |
|---------------------------- |-------------- |---------------:|---------------:|---------------:|---------------:|------:|--------:|-------:|-------:|----------:|
|                  **DirectCall** |         **False** |     **171.316 ns** |      **3.4454 ns** |      **4.4799 ns** |     **169.776 ns** |  **1.00** |    **0.00** | **0.0534** |      **-** |     **224 B** |
|                Interpolated |         False |     772.947 ns |     15.1720 ns |     16.8636 ns |     769.836 ns |  4.50 |    0.15 | 0.0801 |      - |     336 B |
|              IsEnabledCheck |         False |       6.673 ns |      0.1345 ns |      0.1124 ns |       6.659 ns |  0.04 |    0.00 |      - |      - |         - |
|          IsEnabledExtension |         False |       7.683 ns |      0.1490 ns |      0.1393 ns |       7.669 ns |  0.04 |    0.00 |      - |      - |         - |
|         LoggerMessageDefine |         False |       8.823 ns |      0.1949 ns |      0.2166 ns |       8.822 ns |  0.05 |    0.00 |      - |      - |         - |
|             SourceGenerator |         False |       6.948 ns |      0.0869 ns |      0.0725 ns |       6.974 ns |  0.04 |    0.00 |      - |      - |         - |
| SourceGeneratorDynamicLevel |         False |       8.895 ns |      0.1694 ns |      0.1813 ns |       8.872 ns |  0.05 |    0.00 |      - |      - |         - |
|                             |               |                |                |                |                |       |         |        |        |           |
|                  **DirectCall** |          **True** | **273,603.935 ns** | **10,660.1410 ns** | **28,637.7765 ns** | **269,777.490 ns** |  **1.00** |    **0.00** |      **-** |      **-** |   **4,639 B** |
|                Interpolated |          True | 217,654.212 ns |  4,197.0216 ns | 11,129.9060 ns | 216,072.205 ns |  0.80 |    0.08 | 0.4883 | 0.2441 |   3,289 B |
|              IsEnabledCheck |          True | 263,912.188 ns |  5,997.3613 ns | 16,316.2637 ns | 262,161.206 ns |  0.97 |    0.11 | 0.4883 |      - |   4,648 B |
|          IsEnabledExtension |          True | 260,590.562 ns |  7,485.1076 ns | 21,355.4331 ns | 255,825.879 ns |  0.97 |    0.12 |      - |      - |   4,664 B |
|         LoggerMessageDefine |          True | 330,834.852 ns | 31,549.9325 ns | 88,987.1443 ns | 299,255.176 ns |  1.21 |    0.34 | 0.4883 |      - |   5,039 B |
|             SourceGenerator |          True | 327,186.038 ns | 25,803.6035 ns | 72,356.0706 ns | 298,620.898 ns |  1.22 |    0.31 | 0.4883 |      - |   5,088 B |
| SourceGeneratorDynamicLevel |          True | 306,800.817 ns | 20,038.3112 ns | 55,526.0660 ns | 290,559.668 ns |  1.13 |    0.21 | 0.7324 | 0.2441 |   4,720 B |


## A story of how I wasted my day by missing the obvious

Originally this was a long winded post about how the "simplest" option:

#### ~10ns
```csharp
if (_logger.IsEnabled(LogLevel.Information))
{
    _logger.LogInformation(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
}
```

Was significantly faster when dynamic log parameters were involved. All benchmarks I had seen previously were logging constant values and I decided I wanted to represent the "real" world use case with parameters that changed between calls (TLDR; it's a benchmark not the real world):

```csharp
public DateTime Parameter1 => DateTime.UtcNow;

public Guid Parameter2 => Guid.NewGuid();

public int Parameter3 => Random.Shared.Next();
...
```

What I didn't expect was that moving the "simple" that logic into an extension would be significantly slower (TLDR; it's not):

#### ~100ns
```csharp
_logger.Information(Message, Parameter1, Parameter2, Parameter3, Parameter4, Parameter5, Parameter6);
```

```csharp
public static void Information<T1, T2, T3, T4, T5, T6>(this ILogger logger, string message, T1 parameter1, T2 parameter2, T3 parameter3, T4 parameter4, T5 parameter5, T6 parameter6)
{
    if (logger.IsEnabled(LogLevel.Information))
    {
        logger.LogInformation(message, parameter1, parameter2, parameter3, parameter4, parameter5, parameter6);
    }
}
```

I spent a good portion of the day trying to figure out the reasoning behind this significant time difference in my benchmark. I had all these fanicful ideas of how the compiler/runtime were optimizing away the simple check but couldn't quite understand why.

It got to the point where I was having BenchmarkDotNet record the generated assembly code from the benchmarks. While painfully trying to decrypt that I saw my first clue as to the cause, a call to `UpdateLeapYearCache` (or something similar) when the extension method was accessing the DateTime parameter.

Being convinced the problem was not in how I had implemented my benchmark but something else entirely I (ridiculously) decided (based on nothing) that `DateTime.UtcNow` was slow and while I'm at it `Guid.NewGuid()` is probably slow too so lets swap those all to `Random.Shared.Next()` calls because that has to be fast :roll_eyes:

In case you haven't caught it yet I had neglected the fact that if my property getter is generating a new value then that is work that will add to the overhead of the benchmark. I was so focused on what I thought was the problem that I missed the fact that in the "simplest" option I was executing a conditional check **before** accessing the property getters and in every other implementation I had to access them prior executing the check.

At least I wasn't crazy :wink:

My takeaways/reminders from the day are:

- Be willing to consider that your initial assumption may be wrong
- Keep benchmarks targeted and consistent across runs

In any case maybe you'll learn something from my mistake or find use in this code if you want to bootstrap your own logging benchmarks...
