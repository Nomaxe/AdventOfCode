using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe19b : IAufgabe
{
    private readonly int _input;

    public Aufgabe19b()
    {
        _input = Utilities.ReadInputAsT<int>(2016, 19);
    }

    public string Calc()
    {
        var highest3 = (int)Math.Pow(3, Math.Floor(Math.Log(_input) / Math.Log(3)));

        if (_input == highest3)
        {
            return _input.ToString();
        }

        if (_input - highest3 <= highest3)
        {
            return (_input - highest3).ToString();
        }

        return (2 * _input - 3 * highest3).ToString();
    }
}
