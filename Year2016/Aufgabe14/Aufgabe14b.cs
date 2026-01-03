using AdventOfCode.Utils;
using System.Buffers;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2016;

internal partial class Aufgabe14b : IAufgabe
{
    private readonly string _salt;
    private long _index = 0;

    private readonly ArrayPool<string> _pool;
    private string[] _nextHashes;
    private string[] _completedHashes;

    private const int PrecalcArraySize = 1000;

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
    public Aufgabe14b()
    {
        _salt = Utilities.ReadInput(2016, 14)[0];
        _pool = ArrayPool<string>.Create();
    }
#pragma warning restore CS8618

    public string Calc()
    {
        Task.WaitAll(GetNextHashes(0));

        SortedSet<long> validIndex = [];
        DictionaryHashSet<char, long> openedHashes = [];
        long upToIndex = long.MaxValue;

        do
        {
            _completedHashes = _nextHashes;
            var indexForTask = _index;

            var tasks = GetNextHashes(indexForTask + PrecalcArraySize);

            for (int i = 0; i < PrecalcArraySize; i++)
            {
                var hash = _completedHashes[i];
                var char5 = Char5().Match(hash);
                if (char5.Success)
                {
                    openedHashes.RemoveWhere(char5.Value[0], x => x < _index - 1000, false);
                    foreach (var index in openedHashes.GetItems(char5.Value[0]))
                    {
                        validIndex.Add(index);
                    }

                    if (validIndex.Count >= 64 && upToIndex == long.MaxValue)
                    {
                        upToIndex = _index + 1000;
                    }

                    openedHashes.RemoveAll(char5.Value[0]);
                }

                var char3 = Char3().Match(hash);
                if (char3.Success)
                {
                    openedHashes.Add(char3.Value[0], _index);
                }

                _index++;
            }

            Task.WaitAll(tasks);
            _pool.Return(_completedHashes);
            _completedHashes = _nextHashes;
        } while (_index <= upToIndex);

        _pool.Return(_completedHashes);
        return validIndex.ElementAt(63).ToString();
    }

    private Task[] GetNextHashes(long start)
    {
        const int ThreadCount = 5;
        const int SizePerArray = PrecalcArraySize / ThreadCount;

        _nextHashes = _pool.Rent(PrecalcArraySize);
        Task[] tasks = new Task[ThreadCount];

        for (int i = 0; i < ThreadCount; i++)
        {
            var index = i;
            Task task = new(() => CalcHashes(start + index * SizePerArray, index * SizePerArray, SizePerArray));
            task.Start();
            tasks[i] = task;
        }

        return tasks;
    }

    private void CalcHashes(long start, int startIndex, int length)
    {
        for (int i = 0; i < length; i++)
        {
            _nextHashes[startIndex + i] = GetHash($"{_salt}{start + i}");
        }
    }

    private static string GetHash(string hash)
    {
        for (int i = 0; i < 2017; i++)
        {
            hash = Convert.ToHexStringLower(MD5.HashData(Encoding.ASCII.GetBytes(hash)));
        }

        return hash;
    }

    [GeneratedRegex(@"(.)\1{4}")]
    private static partial Regex Char5();
    [GeneratedRegex(@"(.)\1{2}")]
    private static partial Regex Char3();
}
