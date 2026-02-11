using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe03 : IAufgabe
{
    private readonly uint _number;

    public Aufgabe03()
    {
        _number = Utilities.ReadInputAsT<uint>(2017, 3);
    }

    public string Calc()
    {
        var root = (int)Math.Round(Math.Sqrt(_number), MidpointRounding.ToPositiveInfinity);
        var rootEven = root % 2 == 0;
        var rootSquared = root * root;
        var circle = root / 2;
        var line = circle * 2;
        if (rootEven)
        {
            line--;
        }

        bool top = rootEven;
        var corner = rootSquared - line;
        if (_number < corner)
        {
            rootSquared = corner;
            top = false;
        }

        var middle = rootSquared - (top ? circle - 1 : circle);
        var distanceToMiddle = Math.Abs(middle - _number);
        return (distanceToMiddle + circle).ToString();
    }
}
