using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe04 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04()
    {
        _input = Utilities.ReadInput(2023, 4);
    }

    public string Calc()
    {
        const int WinningNumbersCount = 10;
        int result = 0;

        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();
            HashSet<int> winningNumbers = new(WinningNumbersCount);
            int resultLine = 0;

            for (int i = 1; i < numbers.Length; i++)
            {
                if (i <= WinningNumbersCount)
                {
                    winningNumbers.Add(numbers[i]);
                    continue;
                }

                if (winningNumbers.Contains(numbers[i]))
                {
                    if (resultLine == 0)
                    {
                        resultLine = 1;
                    }
                    else
                    {
                        resultLine *= 2;
                    }
                }
            }

            result += resultLine;
        }

        return result.ToString();
    }
}
