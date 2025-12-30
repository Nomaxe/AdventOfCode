using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe06b : IAufgabe
{
    private readonly string _input;

    public Aufgabe06b()
    {
        _input = Utilities.ReadInput(2022, 6)[0];
    }

    public string Calc()
    {
        for (int i = 14; i <= _input.Length; i++)
        {
            if (_input[(i - 14)..i].Distinct().Count() == 14)
            {
                return i.ToString();
            }
        }

        throw new NotImplementedException();
    }
}
