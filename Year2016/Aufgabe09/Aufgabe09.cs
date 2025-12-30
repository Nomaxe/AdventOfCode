using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe09 : IAufgabe
{
    private readonly string _input;

    public Aufgabe09()
    {
        _input = Utilities.ReadInput(2016, 9)[0];
    }

    public string Calc()
    {
        int count = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            if (_input[i] == '(')
            {
                var xIndex = _input.IndexOf('x', i + 1);
                var closingBracketIndex = _input.IndexOf(')', xIndex + 1);
                var characterCount = int.Parse(_input[(i + 1)..xIndex]);
                var times = int.Parse(_input[(xIndex + 1)..closingBracketIndex]);
                count += characterCount * times;
                i = closingBracketIndex + characterCount;
            }
            else
            {
                count++;
            }
        }

        return count.ToString();
    }
}
