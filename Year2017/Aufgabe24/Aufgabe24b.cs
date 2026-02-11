using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe24b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<int, int> _parts;
    private int _maxLength;
    private int _maxStrength;

    public Aufgabe24b()
    {
        _input = Utilities.ReadInput(2017, 24);
        _parts = new(_input.Length * 2);

        _maxLength = 0;
        _maxStrength = 0;
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var numbers = line.GetNumbers();

            _parts.Add(numbers[0], numbers[1]);
            if (numbers[0] != numbers[1])
            {
                _parts.Add(numbers[1], numbers[0]);
            }
        }

        Calc(0, 0, 0, []);

        return _maxStrength.ToString();
    }

    private void Calc(int currentEnd, int length, int strength, HashSet<(int, int)> alreadyAdded)
    {
        foreach (var part in _parts[currentEnd])
        {
            if (alreadyAdded.Contains((currentEnd, part)))
            {
                continue;
            }

            HashSet<(int, int)> newAlreadyAdded = [.. alreadyAdded];
            newAlreadyAdded.Add((currentEnd, part));
            newAlreadyAdded.Add((part, currentEnd));
            Calc(part, length + 1, strength + currentEnd + part, newAlreadyAdded);
        }

        if (length > _maxLength)
        {
            _maxLength = length;
            _maxStrength = strength;
        }
        else if (length == _maxLength)
        {
            _maxStrength = int.Max(_maxStrength, strength);
        }
    }
}
