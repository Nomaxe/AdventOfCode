using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe13b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryDictionary<string, string, int> _happiness;

    public Aufgabe13b()
    {
        _input = Utilities.ReadInput(2015, 13);
        _happiness = new();
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var spaceIndex = line.IndexOf(' ');
            var name = line[..spaceIndex];

            spaceIndex = line.IndexOf(' ', spaceIndex + 1);
            bool negative = line[spaceIndex + 1] == 'l';

            spaceIndex = line.IndexOf(' ', spaceIndex + 1);
            var number = line.GetNumber(spaceIndex + 1);
            if (negative)
            {
                number *= -1;
            }

            spaceIndex = line.LastIndexOf(' ');
            var neighbour = line[(spaceIndex + 1)..^1];

            _happiness.Add(name, neighbour, number);
        }

        int maxHappiness = 0;
        PermuationGenerator<string> generator = new([.. _happiness.Keys, "me"]);
        foreach (var permuation in generator.GetPermuations())
        {
            int happiness = 0;

            for (int i = 0; i < permuation.Count; i++)
            {
                happiness += _happiness.GetValueOrDefault(permuation[i], permuation[(i + 1) % permuation.Count]);
                happiness += _happiness.GetValueOrDefault(permuation[i], permuation[i > 0 ? i - 1 : permuation.Count - 1]);
            }

            maxHappiness = int.Max(maxHappiness, happiness);
        }

        return maxHappiness.ToString();
    }
}
