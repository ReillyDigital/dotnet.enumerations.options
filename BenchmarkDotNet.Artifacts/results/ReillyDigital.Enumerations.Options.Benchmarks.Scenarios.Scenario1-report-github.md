```

BenchmarkDotNet v0.15.8, Linux Pop!_OS 24.04 LTS
AMD Ryzen 5 3600 1.86GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.103
  [Host]    : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3
  .NET 10.0 : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3

Job=.NET 10.0  Runtime=.NET 10.0  IterationTime=100ms  
Iterations=2  

```
| Method                                                         | Categories                                  | Mean        | Ratio    | Allocated | Alloc Ratio |
|--------------------------------------------------------------- |-------------------------------------------- |------------:|---------:|----------:|------------:|
| S1_CheckOptionStringErrorNoMessage_ArdalisResult               | S1: Check Option String Error No Message    |  16.6574 ns |    65.64 |     144 B |          NA |
| S1_CheckOptionStringErrorNoMessage_EnumerationsOptionsBoxed    | S1: Check Option String Error No Message    |   0.2514 ns |     0.99 |         - |          NA |
| S1_CheckOptionStringErrorNoMessage_EnumerationsOptions         | S1: Check Option String Error No Message    |   0.2538 ns |     1.00 |         - |          NA |
| S1_CheckOptionStringErrorNoMessage_FluentResults               | S1: Check Option String Error No Message    | 142.6569 ns |   562.19 |     720 B |          NA |
| S1_CheckOptionStringErrorNoMessage_LightResults                | S1: Check Option String Error No Message    |   0.2669 ns |     1.05 |         - |          NA |
| S1_CheckOptionStringErrorNoMessage_Rascal                      | S1: Check Option String Error No Message    |   8.0556 ns |    31.75 |      48 B |          NA |
| S1_CheckOptionStringErrorNoMessage_SimpleResults               | S1: Check Option String Error No Message    | 262.2819 ns | 1,033.62 |     176 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_CheckOptionStringErrorWithMessage_ArdalisResult             | S1: Check Option String Error With Message  |  43.5667 ns |   189.15 |     208 B |          NA |
| S1_CheckOptionStringErrorWithMessage_EnumerationsOptionsBoxed  | S1: Check Option String Error With Message  |  18.4062 ns |    79.91 |      64 B |          NA |
| S1_CheckOptionStringErrorWithMessage_EnumerationsOptions       | S1: Check Option String Error With Message  |   0.2306 ns |     1.00 |         - |          NA |
| S1_CheckOptionStringErrorWithMessage_FluentResults             | S1: Check Option String Error With Message  | 309.4027 ns | 1,343.28 |    1072 B |          NA |
| S1_CheckOptionStringErrorWithMessage_LightResults              | S1: Check Option String Error With Message  |  20.0742 ns |    87.15 |     112 B |          NA |
| S1_CheckOptionStringErrorWithMessage_Rascal                    | S1: Check Option String Error With Message  |   8.6560 ns |    37.58 |      48 B |          NA |
| S1_CheckOptionStringErrorWithMessage_SimpleResults             | S1: Check Option String Error With Message  | 211.6486 ns |   918.88 |     176 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_CheckOptionStringSuccess_ArdalisResult                      | S1: Check Option String Success             |  15.9497 ns |    66.13 |     144 B |          NA |
| S1_CheckOptionStringSuccess_EnumerationsOptionsBoxed           | S1: Check Option String Success             |   8.4136 ns |    34.89 |      64 B |          NA |
| S1_CheckOptionStringSuccess_EnumerationsOptions                | S1: Check Option String Success             |   0.2412 ns |     1.00 |         - |          NA |
| S1_CheckOptionStringSuccess_FluentResults                      | S1: Check Option String Success             |  68.2100 ns |   282.82 |     320 B |          NA |
| S1_CheckOptionStringSuccess_LightResults                       | S1: Check Option String Success             |   0.2683 ns |     1.11 |         - |          NA |
| S1_CheckOptionStringSuccess_Rascal                             | S1: Check Option String Success             |   0.2683 ns |     1.11 |         - |          NA |
| S1_CheckOptionStringSuccess_SimpleResults                      | S1: Check Option String Success             | 172.3509 ns |   714.62 |     176 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_CheckVoidErrorNoMessage_ArdalisResult                       | S1: Check Void Error No Message             |  17.1838 ns |    62.97 |     144 B |          NA |
| S1_CheckVoidErrorNoMessage_EnumerationsOptionsBoxed            | S1: Check Void Error No Message             |   0.2370 ns |     0.87 |         - |          NA |
| S1_CheckVoidErrorNoMessage_EnumerationsOptions                 | S1: Check Void Error No Message             |   0.2751 ns |     1.01 |         - |          NA |
| S1_CheckVoidErrorNoMessage_FluentResults                       | S1: Check Void Error No Message             | 144.2300 ns |   528.51 |     704 B |          NA |
| S1_CheckVoidErrorNoMessage_LightResults                        | S1: Check Void Error No Message             |   0.2482 ns |     0.91 |         - |          NA |
| S1_CheckVoidErrorNoMessage_Rascal                              | S1: Check Void Error No Message             |   7.4035 ns |    27.13 |      48 B |          NA |
| S1_CheckVoidErrorNoMessage_SimpleResults                       | S1: Check Void Error No Message             | 168.4783 ns |   617.36 |      80 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_CheckVoidErrorWithMessage_ArdalisResult                     | S1: Check Void Error With Message           |  41.6714 ns |   157.45 |     208 B |          NA |
| S1_CheckVoidErrorWithMessage_EnumerationsOptionsBoxed          | S1: Check Void Error With Message           |  19.5394 ns |    73.83 |      64 B |          NA |
| S1_CheckVoidErrorWithMessage_EnumerationsOptions               | S1: Check Void Error With Message           |   0.2659 ns |     1.00 |         - |          NA |
| S1_CheckVoidErrorWithMessage_FluentResults                     | S1: Check Void Error With Message           | 297.2501 ns | 1,123.15 |    1056 B |          NA |
| S1_CheckVoidErrorWithMessage_LightResults                      | S1: Check Void Error With Message           |  19.9530 ns |    75.39 |     112 B |          NA |
| S1_CheckVoidErrorWithMessage_Rascal                            | S1: Check Void Error With Message           |  10.3505 ns |    39.11 |      48 B |          NA |
| S1_CheckVoidErrorWithMessage_SimpleResults                     | S1: Check Void Error With Message           |  94.2676 ns |   356.19 |      80 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_CheckVoidSuccess_ArdalisResult                              | S1: Check Void Success                      |  15.4786 ns |    62.43 |     144 B |          NA |
| S1_CheckVoidSuccess_EnumerationsOptionsBoxed                   | S1: Check Void Success                      |   0.2527 ns |     1.02 |         - |          NA |
| S1_CheckVoidSuccess_EnumerationsOptions                        | S1: Check Void Success                      |   0.2483 ns |     1.00 |         - |          NA |
| S1_CheckVoidSuccess_FluentResults                              | S1: Check Void Success                      |  41.1795 ns |   166.08 |     208 B |          NA |
| S1_CheckVoidSuccess_LightResults                               | S1: Check Void Success                      |   0.2408 ns |     0.97 |         - |          NA |
| S1_CheckVoidSuccess_Rascal                                     | S1: Check Void Success                      |   0.2568 ns |     1.04 |         - |          NA |
| S1_CheckVoidSuccess_SimpleResults                              | S1: Check Void Success                      | 157.6498 ns |   635.83 |      80 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_ReturnOptionStringErrorNoMessage_ArdalisResult              | S1: Return Option String Error No Message   |  16.7574 ns |    63.63 |     144 B |          NA |
| S1_ReturnOptionStringErrorNoMessage_EnumerationsOptionsBoxed   | S1: Return Option String Error No Message   |   0.2559 ns |     0.97 |         - |          NA |
| S1_ReturnOptionStringErrorNoMessage_EnumerationsOptions        | S1: Return Option String Error No Message   |   0.2641 ns |     1.00 |         - |          NA |
| S1_ReturnOptionStringErrorNoMessage_FluentResults              | S1: Return Option String Error No Message   |  93.6854 ns |   355.72 |     544 B |          NA |
| S1_ReturnOptionStringErrorNoMessage_LightResults               | S1: Return Option String Error No Message   |   0.2759 ns |     1.05 |         - |          NA |
| S1_ReturnOptionStringErrorNoMessage_Rascal                     | S1: Return Option String Error No Message   |   8.1961 ns |    31.12 |      48 B |          NA |
| S1_ReturnOptionStringErrorNoMessage_SimpleResults              | S1: Return Option String Error No Message   | 264.5116 ns | 1,004.33 |     176 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_ReturnOptionStringErrorWithMessage_ArdalisResult            | S1: Return Option String Error With Message |  27.5734 ns |   112.24 |     208 B |          NA |
| S1_ReturnOptionStringErrorWithMessage_EnumerationsOptionsBoxed | S1: Return Option String Error With Message |   8.2890 ns |    33.74 |      64 B |          NA |
| S1_ReturnOptionStringErrorWithMessage_EnumerationsOptions      | S1: Return Option String Error With Message |   0.2457 ns |     1.00 |         - |          NA |
| S1_ReturnOptionStringErrorWithMessage_FluentResults            | S1: Return Option String Error With Message |  91.0542 ns |   370.65 |     544 B |          NA |
| S1_ReturnOptionStringErrorWithMessage_LightResults             | S1: Return Option String Error With Message |  20.3513 ns |    82.84 |     112 B |          NA |
| S1_ReturnOptionStringErrorWithMessage_Rascal                   | S1: Return Option String Error With Message |   7.9535 ns |    32.38 |      48 B |          NA |
| S1_ReturnOptionStringErrorWithMessage_SimpleResults            | S1: Return Option String Error With Message | 170.1719 ns |   692.72 |     176 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_ReturnOptionStringNone_ArdalisResult                        | S1: Return Option String None               |  15.6178 ns |    60.15 |     144 B |          NA |
| S1_ReturnOptionStringNone_EnumerationsOptionsBoxed             | S1: Return Option String None               |   0.2377 ns |     0.92 |         - |          NA |
| S1_ReturnOptionStringNone_EnumerationsOptions                  | S1: Return Option String None               |   0.2599 ns |     1.00 |         - |          NA |
| S1_ReturnOptionStringNone_FluentResults                        | S1: Return Option String None               |  43.2558 ns |   166.61 |     224 B |          NA |
| S1_ReturnOptionStringNone_LightResults                         | S1: Return Option String None               |   0.2618 ns |     1.01 |         - |          NA |
| S1_ReturnOptionStringNone_Rascal                               | S1: Return Option String None               |   0.2514 ns |     0.97 |         - |          NA |
| S1_ReturnOptionStringNone_SimpleResults                        | S1: Return Option String None               | 156.9407 ns |   604.49 |      96 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_ReturnOptionStringSuccess_ArdalisResult                     | S1: Return Option String Success            |  14.5822 ns |    57.21 |     144 B |          NA |
| S1_ReturnOptionStringSuccess_EnumerationsOptionsBoxed          | S1: Return Option String Success            |   8.2353 ns |    32.31 |      64 B |          NA |
| S1_ReturnOptionStringSuccess_EnumerationsOptions               | S1: Return Option String Success            |   0.2549 ns |     1.00 |         - |          NA |
| S1_ReturnOptionStringSuccess_FluentResults                     | S1: Return Option String Success            |  43.8614 ns |   172.08 |     224 B |          NA |
| S1_ReturnOptionStringSuccess_LightResults                      | S1: Return Option String Success            |   0.2702 ns |     1.06 |         - |          NA |
| S1_ReturnOptionStringSuccess_Rascal                            | S1: Return Option String Success            |   0.2677 ns |     1.05 |         - |          NA |
| S1_ReturnOptionStringSuccess_SimpleResults                     | S1: Return Option String Success            | 172.9131 ns |   678.39 |     176 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_ReturnVoidErrorNoMessage_ArdalisResult                      | S1: Return Void Error No Message            |  17.1213 ns |    70.71 |     144 B |          NA |
| S1_ReturnVoidErrorNoMessage_EnumerationsOptionsBoxed           | S1: Return Void Error No Message            |   0.2379 ns |     0.98 |         - |          NA |
| S1_ReturnVoidErrorNoMessage_EnumerationsOptions                | S1: Return Void Error No Message            |   0.2424 ns |     1.00 |         - |          NA |
| S1_ReturnVoidErrorNoMessage_FluentResults                      | S1: Return Void Error No Message            |  95.7853 ns |   395.61 |     528 B |          NA |
| S1_ReturnVoidErrorNoMessage_LightResults                       | S1: Return Void Error No Message            |   0.2547 ns |     1.05 |         - |          NA |
| S1_ReturnVoidErrorNoMessage_Rascal                             | S1: Return Void Error No Message            |   7.4922 ns |    30.94 |      48 B |          NA |
| S1_ReturnVoidErrorNoMessage_SimpleResults                      | S1: Return Void Error No Message            | 186.7884 ns |   771.48 |      80 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_ReturnVoidErrorWithMessage_ArdalisResult                    | S1: Return Void Error With Message          |  27.1427 ns |   101.05 |     208 B |          NA |
| S1_ReturnVoidErrorWithMessage_EnumerationsOptionsBoxed         | S1: Return Void Error With Message          |   8.4154 ns |    31.33 |      64 B |          NA |
| S1_ReturnVoidErrorWithMessage_EnumerationsOptions              | S1: Return Void Error With Message          |   0.2686 ns |     1.00 |         - |          NA |
| S1_ReturnVoidErrorWithMessage_FluentResults                    | S1: Return Void Error With Message          |  91.4089 ns |   340.32 |     528 B |          NA |
| S1_ReturnVoidErrorWithMessage_LightResults                     | S1: Return Void Error With Message          |  19.0780 ns |    71.03 |     112 B |          NA |
| S1_ReturnVoidErrorWithMessage_Rascal                           | S1: Return Void Error With Message          |   7.3746 ns |    27.46 |      48 B |          NA |
| S1_ReturnVoidErrorWithMessage_SimpleResults                    | S1: Return Void Error With Message          |  81.8942 ns |   304.89 |      80 B |          NA |
|                                                                |                                             |             |          |           |             |
| S1_ReturnVoidSuccess_ArdalisResult                             | S1: Return Void Success                     |  14.8689 ns |    61.03 |     144 B |          NA |
| S1_ReturnVoidSuccess_EnumerationsOptionsBoxed                  | S1: Return Void Success                     |   0.2476 ns |     1.02 |         - |          NA |
| S1_ReturnVoidSuccess_EnumerationsOptions                       | S1: Return Void Success                     |   0.2437 ns |     1.00 |         - |          NA |
| S1_ReturnVoidSuccess_FluentResults                             | S1: Return Void Success                     |  20.1149 ns |    82.56 |     112 B |          NA |
| S1_ReturnVoidSuccess_LightResults                              | S1: Return Void Success                     |   0.2271 ns |     0.93 |         - |          NA |
| S1_ReturnVoidSuccess_Rascal                                    | S1: Return Void Success                     |   0.2493 ns |     1.02 |         - |          NA |
| S1_ReturnVoidSuccess_SimpleResults                             | S1: Return Void Success                     | 152.6964 ns |   626.72 |      80 B |          NA |
