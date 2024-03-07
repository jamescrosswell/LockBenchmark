using System.Collections.Concurrent;
using BenchmarkDotNet.Attributes;

namespace LockBenchmark;

public class LockBenchmarks
{
    private static readonly long Period = TimeSpan.FromMilliseconds(1000).Ticks;
    private const int Parallelism = 100;
    private Hole _hole;
    
    [GlobalSetup]
    public void GlobalSetup()
    {
        _hole = new Hole(LockedWork, UnLockedWork);
    }

    [Params(10, 100)]
    public int LockedWork { get; set; }

    private const int UnLockedWork = 10;
    
    static LockBenchmarks()
    {
        ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);
    }
    
    private void PrintResults() => Console.WriteLine($"Depth: {_hole.Depth}");
    
    [Benchmark]
    public void Lock()
    {
        long lastReset = 0;
        var resetLock = new object();
        
        Parallel.For(0, Parallelism, i =>
        {
            _hole.Dig();

            lock (resetLock)
            {
                if (DateTime.UtcNow.Ticks - lastReset > Period)
                {
                    lastReset = DateTime.UtcNow.Ticks;
                    _hole.Fill();
                }
            }
        });

        PrintResults();
    }

    [Benchmark]
    public void Interlock()
    {
        long lastReset = 0;
        
        Parallel.For(0, Parallelism, i =>
        {
            _hole.Dig();
            
            var reset = Interlocked.Read(ref lastReset);
            if (DateTime.UtcNow.Ticks - reset > Period)
            {
                if (Interlocked.CompareExchange(ref lastReset, DateTime.UtcNow.Ticks, reset) == reset)
                {
                    _hole.Fill();
                }
            }
        });
        
        PrintResults();
    }

    [Benchmark]
    public void SemaphoreSlim()
    {
        long lastReset = 0;
        var ss = new SemaphoreSlim(1, 1);
        
        Parallel.For(0, Parallelism, i =>
        {
            _hole.Dig();
            
            ss.Wait();
            try
            {
                if (DateTime.UtcNow.Ticks - lastReset > Period)
                {
                    lastReset = DateTime.UtcNow.Ticks;
                    _hole.Fill();
                }
            }
            finally
            {
                ss.Release();
            }
        });
        
        PrintResults();
    }
}

public class Hole(int lockedWork, int unLockedWork)
{
    private long _depth = 0;
    public long Depth => Interlocked.Read(ref _depth);
    public void Dig()
    {
        Thread.Sleep(unLockedWork); // Fake work
        Interlocked.Decrement(ref _depth);
    }
    public void Fill()
    {
        Thread.Sleep(lockedWork); // Fake work
        Interlocked.Exchange(ref _depth, 0);
    }
}