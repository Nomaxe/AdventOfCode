using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe09b : IAufgabe
{
    public readonly Dictionary<(string From, string To), int> _distance = [];
    private readonly HashSet<string> _towns = [];

    public Aufgabe09b()
    {
        var input = Utilities.ReadInput(2015, 9);
        foreach (var line in input)
        {
            var split = line.Split(' ');
            var distance = int.Parse(split[^1]);
            _distance.Add((split[0], split[2]), distance);
            _distance.Add((split[2], split[0]), distance);
            _towns.Add(split[0]);
            _towns.Add(split[2]);
        }
    }

    public string Calc()
    {
        int result = 0;

        foreach (var town in _towns)
        {
            result = int.Max(Calc(town, _towns.Where(x => x != town).ToList(), 0), result);
        }

        return result.ToString();
    }

    public int Calc(string currentTown, List<string> remainingTowns, int currentLength)
    {
        bool otherTownFound = false;

        if (remainingTowns.Count == 0)
        {
            return currentLength;
        }

        int length = 0;

        for (int i = 0; i < remainingTowns.Count; i++)
        {
            if (_distance.TryGetValue((currentTown, remainingTowns[i]), out var distanceToTown))
            {
                otherTownFound = true;
                length = int.Max(Calc(remainingTowns[i], [.. remainingTowns[..i], .. remainingTowns[(i + 1)..]], currentLength + distanceToTown), length);
            }
        }

        if (otherTownFound)
        {
            return length;
        }
        else
        {
            return currentLength;
        }
    }
}
