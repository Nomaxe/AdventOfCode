using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe04b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04b()
    {
        _input = Utilities.ReadInput(2023, 4);
    }

    public string Calc()
    {
        const int WinningNumbersCount = 10;
        LargeCounter<int> counter = new(_input.Length);


        //Orginale
        for (int i = 0; i < _input.Length; i++)
        {
            counter.Add(i);
        }

        //Kopien
        for (int i = 0; i < _input.Length; i++)
        {
            var numbers = _input[i].GetUnsignedNumbers();
            HashSet<int> winningNumbers = new(WinningNumbersCount);
            int winningCount = 0;

            for (int j = 1; j < numbers.Length; j++)
            {
                if (j <= WinningNumbersCount)
                {
                    winningNumbers.Add(numbers[j]);
                    continue;
                }

                if (winningNumbers.Contains(numbers[j]))
                {
                    winningCount++;
                }
            }

            var currentCount = counter[numbers[0]];
            for (int j = 1; j <= winningCount; j++)
            {
                counter.Add(numbers[0] + j, currentCount);
            }
        }

        return counter.GetTotalCount().ToString();
    }
}
