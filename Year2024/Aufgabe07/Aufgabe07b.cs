using AdventOfCode.Utils;
using System.Buffers;

namespace AdventOfCode.Year2024;

internal class Aufgabe07b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Equation> _equations;
    private readonly long[] _result;
    private readonly ArrayPool<char> _pool;
    private readonly ArrayPool<long> _longPool;

    private const int ThreadCount = 5;

    public Aufgabe07b()
    {
        _input = Utilities.ReadInput(2024, 7);
        _equations = new(_input.Length);
        _pool = ArrayPool<char>.Shared;
        _longPool = ArrayPool<long>.Shared;
        _result = _longPool.Rent(ThreadCount);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(' ');
            var result = long.Parse(split[0][..^1]);
            List<long> numbers = [];
            for (int i = 1; i < split.Length; i++)
            {
                numbers.Add(long.Parse(split[i]));
            }

            _equations.Add(new(result, numbers));
        }

        Task[] tasks = new Task[ThreadCount];
        var calcAmount = _equations.Count / ThreadCount;
        for (int i = 0; i < tasks.Length; i++)
        {
            var index = i;
            tasks[i] = new(() => GetResults(index, index * calcAmount, (index + 1) * calcAmount - 1));
            tasks[i].Start();
        }

        Task.WaitAll(tasks);

        return _result.Sum().ToString();
    }

    private void GetResults(int index, int from, int to)
    {
        for (int i = from; i <= to; i++)
        {
            if (GetResult(_equations[i]))
            {
                _result[index] += _equations[i].Result;
            }
        }
    }

    private bool GetResult(Equation equation)
    {
        var arrayLength = equation.Numbers.Count - 1;
        var operators = _pool.Rent(arrayLength);
        for (int i = 0; i < arrayLength; i++)
        {
            operators[i] = '+';
        }

        var countAmount = (int)Math.Pow(3, arrayLength);
        for (int count = 0; count < countAmount; count++)
        {
            long result = equation.Numbers[0];
            for (int i = 0; i < arrayLength; i++)
            {
                var number = equation.Numbers[i + 1];

                if (operators[i] == '+')
                {
                    result += number;
                }
                else if (operators[i] == '*')
                {
                    result *= number;
                }
                else
                {
                    result = long.Parse(result.ToString() + number.ToString());
                }
            }

            if (result == equation.Result)
            {
                _pool.Return(operators);
                return true;
            }

            for (int i = 0; i < arrayLength; i++)
            {
                if (operators[i] == '+')
                {
                    operators[i] = '*';
                    break;
                }
                else if (operators[i] == '*')
                {
                    operators[i] = '|';
                    break;
                }

                operators[i] = '+';
            }
        }

        _pool.Return(operators);
        return false;
    }

    private record struct Equation(long Result, List<long> Numbers);
}
