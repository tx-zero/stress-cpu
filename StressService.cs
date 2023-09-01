
using System.Diagnostics;

namespace stress_cpu;

public class StressService : IHostedService
{
    private static volatile int _percent = 0;

    public static int Percent
    {
        get { return _percent; }
        set
        {
            if (value < 0 || value > 100)
            {
                throw new ArgumentException("Percent should be between 0 and 100.");
            }
            _percent = value;
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        for (int i = 0; i < Environment.ProcessorCount; i++)
        {
            new Thread(() =>
            {
                var stopwatch = new Stopwatch();
                while (true)
                {
                    if (_percent == 0)
                    {
                        Thread.Sleep(1000);
                    }
                    else
                    {
                        stopwatch.Restart();
                        while (stopwatch.ElapsedMilliseconds < _percent) { } // hard work
                        Thread.Sleep(100 - _percent); // chill out
                    }
                }
            }).Start();
        }
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
