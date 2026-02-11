using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe09 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<(string From, string To), int> _distance;
    private readonly HashSet<string> _towns;

    public Aufgabe09()
    {
        _input = Utilities.ReadInput(2015, 9);
        _distance = new(_input.Length);
        _towns = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(' ');
            var distance = int.Parse(split[^1]);
            _distance.Add((split[0], split[2]), distance);
            _distance.Add((split[2], split[0]), distance);
            _towns.Add(split[0]);
            _towns.Add(split[2]);
        }

        int result = int.MaxValue;

        foreach (var town in _towns)
        {
            result = int.Min(Calc(town, _towns.Where(x => x != town).ToList(), 0), result);
        }

        return result.ToString();
    }

    public int Calc(string currentTown, List<string> remainingTowns, int currentLength)
    {
        if (remainingTowns.Count == 0)
        {
            return currentLength;
        }

        int length = int.MaxValue - currentLength;

        for (int i = 0; i < remainingTowns.Count; i++)
        {
            if (_distance.TryGetValue((currentTown, remainingTowns[i]), out var distanceToTown))
            {
                length = int.Min(Calc(remainingTowns[i], [.. remainingTowns[..i], .. remainingTowns[(i + 1)..]], currentLength + distanceToTown), length);
            }
        }

        return length;
    }
}
