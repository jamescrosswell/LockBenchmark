```

BenchmarkDotNet v0.13.12, macOS Sonoma 14.2.1 (23C71) [Darwin 23.2.0]
Apple M2 Max, 1 CPU, 12 logical and 12 physical cores
.NET SDK 8.0.100
  [Host]   : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD
  ShortRun : .NET 8.0.0 (8.0.23.53103), Arm64 RyuJIT AdvSIMD

Job=ShortRun  IterationCount=3  LaunchCount=1  
WarmupCount=3  

```
| Method        | LockedWork | Mean      | Error     | StdDev   |
|-------------- |----------- |----------:|----------:|---------:|
| **Lock**          | **10**         |  **97.00 ms** |  **5.578 ms** | **0.306 ms** |
| Interlock     | 10         |  86.88 ms |  1.715 ms | 0.094 ms |
| SemaphoreSlim | 10         |  97.62 ms |  5.045 ms | 0.277 ms |
| **Lock**          | **100**        | **187.58 ms** |  **8.191 ms** | **0.449 ms** |
| Interlock     | 100        | 143.36 ms | 92.690 ms | 5.081 ms |
| SemaphoreSlim | 100        | 187.67 ms | 13.449 ms | 0.737 ms |
