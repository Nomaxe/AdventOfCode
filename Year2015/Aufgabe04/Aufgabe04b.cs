using AdventOfCode.Utils;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2015;

internal class Aufgabe04b : IAufgabe
{
    private readonly string _key;
    private int _currentIndex;

    private const int ThreadCount = 5;
    private const int ThreadSize = 50000;

    private int _smallestNumber = int.MaxValue;
    private readonly Lock _lock;

    public Aufgabe04b()
    {
        _key = Utilities.ReadInput(2015, 4)[0];
        _currentIndex = 0;
        _lock = new();
    }

    public string Calc()
    {
        Task[] tasks = new Task[ThreadCount];

        for (int i = 0; i < ThreadCount; i++)
        {
            tasks[i] = Task.Factory.StartNew(Test, TaskCreationOptions.LongRunning);
        }

        Task.WaitAll(tasks);
        return _smallestNumber.ToString();
    }

    private void Test()
    {
        do
        {
            int from, to;

            lock (_lock)
            {
                from = _currentIndex;
                _currentIndex += ThreadSize;
                to = _currentIndex - 1;
            }

            for (int i = from; i <= to; i++)
            {
                if (_currentIndex > _smallestNumber)
                {
                    return;
                }

                var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{_key}{i}"));
                if (hash[0] == 0 && hash[1] == 0 && hash[2] == 0)
                {
                    lock (_lock)
                    {
                        _smallestNumber = int.Min(_smallestNumber, i);
                    }
                }
            }
        }
        while (true);
    }
}
