using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe25 : IAufgabe
{
    private readonly int _row;
    private readonly int _column;

    public Aufgabe25()
    {
        var numbers = Utilities.ReadInput(2015, 25)[0].GetNumbers();
        _row = numbers[0];
        _column = numbers[1];
    }

    public string Calc()
    {
        var startRow = _row + _column - 1;
        var times = 1;

        for (int i = 2; i <= startRow; i++)
        {
            times += i - 1;
        }

        times += _column - 1;
        long result = 20151125;

        for (int i = 1; i < times; i++)
        {
            result = result * 252533 % 33554393;
        }

        return result.ToString();
    }
}
