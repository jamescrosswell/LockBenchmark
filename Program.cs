using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using LockBenchmark;

BenchmarkRunner.Run<LockBenchmarks>(DefaultConfig.Instance.AddJob(Job.ShortRun));