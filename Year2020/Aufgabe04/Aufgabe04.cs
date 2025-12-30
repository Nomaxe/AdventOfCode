using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe04 : IAufgabe
{
    private readonly string[] _input;
    public Aufgabe04()
    {
        _input = Utilities.ReadInput(2020, 4);
    }

    public string Calc()
    {
        int count = 0;
        int currentCount = 0;

        foreach (var line in _input.Append(string.Empty))
        {
            if (string.IsNullOrEmpty(line))
            {
                if (currentCount == 7)
                {
                    count++;
                }

                currentCount = 0;
                continue;
            }

            var split = line.Split(' ');
            foreach (var item in split)
            {
                var field = item.AsSpan(0, 3);
                if (!MemoryExtensions.Equals(field, "cid", StringComparison.InvariantCulture))
                {
                    currentCount++;
                }
            }
        }

        return count.ToString();
    }
}
