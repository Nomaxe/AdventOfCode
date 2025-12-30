using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe11 : IAufgabe
{
    private readonly int _serialnumber;
    private readonly GridInt _grid;

    public Aufgabe11()
    {
        _serialnumber = int.Parse(Utilities.ReadInput(2018, 11)[0]);
        _grid = new(300);
    }

    public string Calc()
    {
        FillGrid();
        var max = GetBiggestArea();
        return $"{max.X + 1},{max.Y + 1}"; //Grid fängt bei 1,1 an...
    }

    private void FillGrid()
    {
        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                var rackID = x + 1 + 10;
                var powerLevel = rackID * (y + 1);
                var powerLevelWithSerialnumber = powerLevel + _serialnumber;
                var withRackID = powerLevelWithSerialnumber * rackID;
                var digit = withRackID / 100 % 10;
                _grid.SetValue(x, y, digit - 5);
            }
        }
    }

    private Point GetBiggestArea()
    {
        var maxX = 0;
        var maxY = 0;
        var maxValue = 0;

        for (int y = 0; y < _grid.SizeY - 2; y++)
        {
            for (int x = 0; x < _grid.SizeX - 2; x++)
            {
                var areaValue = GetAreaValue(x, y);
                if (areaValue > maxValue)
                {
                    maxX = x;
                    maxY = y;
                    maxValue = areaValue;
                }
            }
        }

        return new(maxX, maxY);
    }

    private int GetAreaValue(int x, int y)
    {
        return _grid.GetValue(x, y) + _grid.GetValue(x + 1, y) + _grid.GetValue(x + 2, y) +
               _grid.GetValue(x, y + 1) + _grid.GetValue(x + 1, y + 1) + _grid.GetValue(x + 2, y + 1) +
               _grid.GetValue(x, y + 2) + _grid.GetValue(x + 1, y + 2) + _grid.GetValue(x + 2, y + 2);
    }
}
