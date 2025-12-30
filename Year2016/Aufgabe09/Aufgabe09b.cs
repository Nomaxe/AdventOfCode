using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe09b : IAufgabe
{
    private readonly string _input;
    private ulong _count;

    public Aufgabe09b()
    {
        _input = Utilities.ReadInput(2016, 9)[0];
    }

    public string Calc()
    {
        Calc(_input, 1);

        return _count.ToString();
    }

    private void Calc(string input, ulong multiplicator)
    {
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == '(')
            {
                var xIndex = input.IndexOf('x', i + 1);
                var closingBracketIndex = input.IndexOf(')', xIndex + 1);
                var characterCount = int.Parse(input[(i + 1)..xIndex]);
                var times = ulong.Parse(input[(xIndex + 1)..closingBracketIndex]);
                Calc(input[(closingBracketIndex + 1)..(closingBracketIndex + characterCount + 1)], multiplicator * times);
                i = closingBracketIndex + characterCount;
            }
            else
            {
                _count += multiplicator;
            }
        }
    }
}
