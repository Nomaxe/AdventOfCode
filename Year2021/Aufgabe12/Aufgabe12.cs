using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe12 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _caves;
    private int _count;

    public Aufgabe12()
    {
        _input = Utilities.ReadInput(2021, 12);
        _caves = [];
        _count = 0;
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split('-');
            _caves.Add(split[0], split[1]);
            _caves.Add(split[1], split[0]);
        }

        Calc("start", []);

        return _count.ToString();
    }

    private void Calc(string currentCave, List<string> visitedCaves)
    {
        var connectedCaves = _caves[currentCave];
        visitedCaves.Add(currentCave);

        foreach (var connectedCave in connectedCaves)
        {
            if (connectedCave == "end")
            {
                _count++;
                continue;
            }

            if (connectedCave.IsLowerCase())
            {
                if (visitedCaves.Contains(connectedCave))
                {
                    continue;
                }
            }

            Calc(connectedCave, [.. visitedCaves]);
        }
    }
}
