```

BenchmarkDotNet v0.13.12, macOS Sonoma 14.2.1 (23C71) [Darwin 23.2.0]
Apple M2 Max, 1 CPU, 12 logical and 12 physical cores
.NET SDK 8.0.100
  [Host]   : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method        | FillTime | Mean      | Error      | StdDev   |
|-------------- |--------- |----------:|-----------:|---------:|
| **Lock**          | **10**       |  **86.78 ms** |   **7.175 ms** | **0.393 ms** |
| Interlock     | 10       |  88.21 ms |  40.987 ms | 2.247 ms |
| SemaphoreSlim | 10       |  87.05 ms |   3.873 ms | 0.212 ms |
| **Lock**          | **100**      | **135.48 ms** | **110.080 ms** | **6.034 ms** |
| Interlock     | 100      | 143.78 ms | 140.779 ms | 7.717 ms |
| SemaphoreSlim | 100      | 140.48 ms |  43.458 ms | 2.382 ms |
