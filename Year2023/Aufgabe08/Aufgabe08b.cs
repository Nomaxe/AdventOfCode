using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe08b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _instructions;

    public Aufgabe08b()
    {
        _input = Utilities.ReadInput(2023, 8);
        _instructions = new(_input.Length - 2);
    }

    public string Calc()
    {
        foreach (var line in _input.Skip(2))
        {
            var parent = line[..3];

            _instructions.Add(parent, line[7..10]);
            _instructions.Add(parent, line[12..15]);
        }

        List<ulong> counts = [];
        foreach (var startposition in _instructions.Keys.Where(x => x[2] == 'A'))
        {
            counts.Add(GetCount(startposition));
        }

        return MathEnhancement.GetLowestCommonMultiple(counts).ToString();
    }

    private ulong GetCount(string position)
    {
        ulong count = 0;
        string currentPosition = position;

        while (true)
        {
            foreach (var direction in _input[0])
            {
                currentPosition = _instructions[currentPosition][direction == 'L' ? 0 : 1];

                count++;

                if (currentPosition[2] == 'Z')
                {
                    return count;
                }
            }
        }
    }
}
