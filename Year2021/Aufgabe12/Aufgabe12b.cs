using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe12b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _caves;
    private int _count;

    public Aufgabe12b()
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
            if (split[1] != "start")
            {
                _caves.Add(split[0], split[1]);
            }
            if (split[0] != "start")
            {
                _caves.Add(split[1], split[0]);
            }
        }

        Calc("start", [], false);

        return _count.ToString();
    }

    private void Calc(string currentCave, List<string> visitedCaves, bool smallCaveVisitedTwice)
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

            var smallCaveVisitedTwiceNew = smallCaveVisitedTwice;
            if (connectedCave.IsLowerCase())
            {
                if (visitedCaves.Contains(connectedCave))
                {
                    if (smallCaveVisitedTwice)
                    {
                        continue;
                    }
                    else
                    {
                        smallCaveVisitedTwiceNew = true;
                    }
                }
            }

            Calc(connectedCave, [.. visitedCaves], smallCaveVisitedTwiceNew);
        }
    }
}
