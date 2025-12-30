using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe08 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe08()
    {
        _input = Utilities.ReadInput(2023, 8);
    }

    public string Calc()
    {
        DictionaryList<string, string> instructions = new(_input.Length - 2);

        foreach (var line in _input.Skip(2))
        {
            var parent = line[..3];

            instructions.Add(parent, line[7..10]);
            instructions.Add(parent, line[12..15]);
        }

        string currentPosition = "AAA";
        const string EndPosition = "ZZZ";
        int count = 0;

        while (true)
        {
            foreach (var direction in _input[0])
            {
                currentPosition = instructions[currentPosition][direction == 'L' ? 0 : 1];
                count++;

                if (currentPosition == EndPosition)
                {
                    return count.ToString();
                }
            }
        }
    }
}
