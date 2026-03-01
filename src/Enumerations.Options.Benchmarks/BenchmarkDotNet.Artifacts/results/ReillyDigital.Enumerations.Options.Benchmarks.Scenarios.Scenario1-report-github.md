```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.26200.7840/25H2/2025Update/HudsonValley2)
AMD Ryzen 5 3600 3.59GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.103
  [Host]    : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3
  .NET 10.0 : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3

Job=.NET 10.0  Runtime=.NET 10.0  IterationTime=100ms  
Iterations=2  

```
| Method                                                    | Categories                                  | Mean      | Ratio | Allocated | Alloc Ratio |
|---------------------------------------------------------- |-------------------------------------------- |----------:|------:|----------:|------------:|
| S1_CheckOptionStringErrorNoMessage_EnumerationsOptions    | S1: Check Option String Error No Message    | 1.9384 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_CheckOptionStringErrorWithMessage_EnumerationsOptions  | S1: Check Option String Error With Message  | 0.3016 ns |  1.01 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_CheckOptionStringSuccess_EnumerationsOptions           | S1: Check Option String Success             | 0.3886 ns |  1.02 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_CheckVoidErrorNoMessage_EnumerationsOptions            | S1: Check Void Error No Message             | 0.1773 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_CheckVoidErrorWithMessage_EnumerationsOptions          | S1: Check Void Error With Message           | 0.2163 ns |  1.01 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_CheckVoidSuccess_EnumerationsOptions                   | S1: Check Void Success                      | 0.2254 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_ReturnOptionStringErrorNoMessage_EnumerationsOptions   | S1: Return Option String Error No Message   | 0.2437 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_ReturnOptionStringErrorWithMessage_EnumerationsOptions | S1: Return Option String Error With Message | 0.2382 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_ReturnOptionStringNone_EnumerationsOptions             | S1: Return Option String None               | 0.2290 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_ReturnOptionStringSuccess_EnumerationsOptions          | S1: Return Option String Success            | 0.2422 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_ReturnVoidErrorNoMessage_EnumerationsOptions           | S1: Return Void Error No Message            | 0.2395 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_ReturnVoidErrorWithMessage_EnumerationsOptions         | S1: Return Void Error With Message          | 0.2569 ns |  1.00 |         - |          NA |
|                                                           |                                             |           |       |           |             |
| S1_ReturnVoidSuccess_EnumerationsOptions                  | S1: Return Void Success                     | 0.2358 ns |  1.00 |         - |          NA |
