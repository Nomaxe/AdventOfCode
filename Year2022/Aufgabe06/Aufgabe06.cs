using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe06 : IAufgabe
{
    private readonly string _input;

    public Aufgabe06()
    {
        _input = Utilities.ReadInputAsString(2022, 6);
    }

    public string Calc()
    {
        for (int i = 4; i <= _input.Length; i++)
        {
            if (_input[(i - 4)..i].Distinct().Count() == 4)
            {
                return i.ToString();
            }
        }

        throw new NotImplementedException();
    }
}
