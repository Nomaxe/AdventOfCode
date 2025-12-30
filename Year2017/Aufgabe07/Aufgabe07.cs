using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe07 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _programs;

    public Aufgabe07()
    {
        _input = Utilities.ReadInput(2017, 7);
        _programs = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var whitespace = line.IndexOf(' ');
            var program = line[..whitespace];

            var subprogramsIndex = line.IndexOf("->");
            if (subprogramsIndex >= 0)
            {
                var subprograms = line[(subprogramsIndex + 3)..].Split(", ");
                _programs.Add(program, subprograms);
            }
            else
            {
                _programs.AddKey(program);
            }
        }

        return GetStart();
    }

    private string GetStart()
    {
        HashSet<string> start = new(_programs.Count);
        foreach (var key in _programs.Keys)
        {
            start.Add(key);
        }

        foreach (var program in _programs)
        {
            foreach (var subprogram in program.Value)
            {
                start.Remove(subprogram);
            }
        }

        return start.First();
    }
}
