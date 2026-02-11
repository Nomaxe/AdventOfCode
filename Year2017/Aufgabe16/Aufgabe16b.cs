using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe16b : IAufgabe
{
    private List<char> _programs = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'];
    private readonly List<char> _sortedPrograms = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'];
    private readonly Dictionary<int, List<char>> _programsAfterDance = [];
    private readonly List<string> _dance = [];

    public Aufgabe16b()
    {
        _dance = Utilities.ReadInputAsList<string>(2017, 16, ',');
        _programsAfterDance.Add(0, _sortedPrograms);
    }

    public string Calc()
    {
        for (int i = 0; i < 1_000_000_000; i++)
        {
            foreach (var dance in _dance)
            {
                switch (dance[0])
                {
                    case 's':
                        var count = int.Parse(dance[1..]);
                        List<char> nextProgams = new(_programs.Count);
                        nextProgams.AddRange(_programs[^count..]);
                        nextProgams.AddRange(_programs[..^count]);
                        _programs = nextProgams;
                        break;
                    case 'x':
                        var index = dance.IndexOf('/');
                        var firstIndex = int.Parse(dance[1..index]);
                        var secondIndex = int.Parse(dance[(index + 1)..]);
                        (_programs[firstIndex], _programs[secondIndex]) = (_programs[secondIndex], _programs[firstIndex]);
                        break;
                    case 'p':
                        firstIndex = _programs.IndexOf(dance[1]);
                        secondIndex = _programs.IndexOf(dance[3]);
                        (_programs[firstIndex], _programs[secondIndex]) = (_programs[secondIndex], _programs[firstIndex]);
                        break;
                    default:
                        throw new NotImplementedException();
                }
                ;
            }

            if (_programs.SequenceEqual(_sortedPrograms))
            {
                break;
            }

            _programsAfterDance.Add(i + 1, [.. _programs]);
        }

        return string.Join("", _programsAfterDance[1_000_000_000 % _programsAfterDance.Count]);
    }
}
