using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe05 : IAufgabe
{
    private readonly List<int> _code;
    private int _pointer = 0;

    public Aufgabe05()
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
            _code[_pointer]++;
            _pointer += nextPointer;

            steps++;
        } while (_pointer < _code.Count);

        return steps.ToString();
    }
}
