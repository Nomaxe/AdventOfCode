using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe22 : IAufgabe
{
    private readonly int _depth;
    private readonly Point _target;
    private readonly GridInt _erosionLevel;
    private int _riskFactor;

    public Aufgabe22()
    {
        var input = Utilities.ReadInput(2018, 22);
        _depth = input[0].GetNumber(7);
        _target = new(input[1].GetNumbers());
        _erosionLevel = new(_target.X + 1, _target.Y + 1);
        _riskFactor = 0;
    }

    public string Calc()
    {
        for (int y = 0; y <= _target.Y; y++)
        {
            for (int x = 0; x <= _target.X; x++)
            {
                CalcErosionLevel(x, y);
            }
        }

        return _riskFactor.ToString();
    }

    private void CalcErosionLevel(int x, int y)
    {
        int geologicIndex;

        if (x == 0 && y == 0)
        {
            geologicIndex = 0;
        }
        else if (x == _target.X && y == _target.Y)
        {
            geologicIndex = 0;
        }
        else if (y == 0)
        {
            geologicIndex = x * 16807;
        }
        else if (x == 0)
        {
            geologicIndex = y * 48271;
        }
        else
        {
            geologicIndex = _erosionLevel.GetValue(x - 1, y) * _erosionLevel.GetValue(x, y - 1);
        }

        var erosionLevel = (geologicIndex + _depth) % 20183;
        _erosionLevel.SetValue(x, y, erosionLevel);

        var type = erosionLevel % 3;

        _riskFactor += type;
    }
}
