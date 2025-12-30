using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe05 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<List<long[]>> _numbers;

    public Aufgabe05()
    {
        _input = Utilities.ReadInput(2023, 5);
        _numbers = [];
    }

    public string Calc()
    {
        var seeds = _input[0].GetUnsignedLongNumbers();
        List<long[]> currentList = [];
        _numbers.Add(currentList);

        for (int i = 3; i < _input.Length; i++)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                i++;
                currentList = [];
                _numbers.Add(currentList);
                continue;
            }

            currentList.Add(_input[i].GetUnsignedLongNumbers());
        }

        var min = long.MaxValue;

        foreach (var seed in seeds)
        {
            var currentNumber = seed;

            foreach (var numbers in _numbers)
            {
                currentNumber = GetNumber(currentNumber, numbers);
            }

            min = long.Min(min, currentNumber);

            if (seed % 100_000 == 0)
            {
                Console.WriteLine($"{seed:N0} => {currentNumber:N0} / {min:N0}");
            }
        }

        return min.ToString();
    }

    private static long GetNumber(long seed, List<long[]> numbersList)
    {
        foreach (var numbers in numbersList)
        {
            if (seed >= numbers[1] && seed < numbers[1] + numbers[2])
            {
                var offset = seed - numbers[1];
                return numbers[0] + offset;
            }
        }

        return seed;
    }
}
