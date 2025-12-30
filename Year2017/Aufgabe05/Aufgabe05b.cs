using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe05b : IAufgabe
{
    private readonly List<int> _code;
    private int _pointer = 0;

    public Aufgabe05b()
    {
        var input = Utilities.ReadInput(2017, 5);
        _code = new(input.Length);
        foreach (var line in input)
        {
            _code.Add(int.Parse(line));
        }
    }

    public string Calc()
    {
        int steps = 0;

        do
        {
            var nextPointer = _code[_pointer];
            if (nextPointer >= 3)
            {
                _code[_pointer]--;
            }
            else
            {
                _code[_pointer]++;
            }
            _pointer += nextPointer;

            steps++;
        } while (_pointer < _code.Count);

        return steps.ToString();
    }
}
