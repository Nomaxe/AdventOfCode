using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe05b : IAufgabe
{
    private readonly string _polymer;
    private readonly int[] _counts;

    private const int ThreadCount = 6;


    public Aufgabe05b()
    {
        _polymer = Utilities.ReadInput(2018, 5)[0];
        _counts = new int[ThreadCount];
    }

    public string Calc()
    {
        Task[] tasks =
        [
            new(() => Calc(0, 'A', 'D')),
            new(() => Calc(1, 'E', 'G')),
            new(() => Calc(2, 'H', 'K')),
            new(() => Calc(3, 'L', 'P')),
            new(() => Calc(4, 'Q', 'U')),
            new(() => Calc(5, 'V', 'Z')),
        ];
        foreach (var task in tasks)
        {
            task.Start();
        }
        Task.WaitAll(tasks);

        return _counts.Min().ToString();
    }

    private void Calc(int index, char from, char to)
    {
        _counts[index] = int.MaxValue;

        for (int i = from; i <= to; i++)
        {
            char character = (char)i;
            _counts[index] = int.Min(GetLength(_polymer.Where(x => char.ToUpper(x) != character).ToList()), _counts[index]);
        }
    }

    private static int GetLength(List<char> input)
    {
        bool didChange;

        do
        {
            List<char> nextInput = new(input.Count);
            didChange = false;

            for (int i = 0; i < input.Count; i++)
            {
                if (i == input.Count - 1)
                {
                    nextInput.Add(input[i]);
                }
                else if (!(input[i] != input[i + 1] && char.ToUpper(input[i]) == char.ToUpper(input[i + 1])))
                {
                    nextInput.Add(input[i]);
                }
                else
                {
                    didChange = true;
                    i++;
                }
            }

            input = nextInput;
        } while (didChange);

        return input.Count;
    }
}
