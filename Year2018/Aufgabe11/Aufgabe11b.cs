using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe11b : IAufgabe
{
    private readonly int _serialnumber;
    private readonly GridInt _grid;
    private readonly Dictionary<Area, int> _areaSize;

    public Aufgabe11b()
    {
        _serialnumber = int.Parse(Utilities.ReadInput(2018, 11)[0]);
        _grid = new(300);
        _areaSize = [];
    }

    public string Calc()
    {
        FillGrid();
        for (int i = 2; i <= 300; i++)
        {
            if (!CalcArea(i))
            {
                break;
            }

        }

        var max = _areaSize.MaxBy(x => x.Value).Key;
        return $"{max.X + 1},{max.Y + 1},{max.Size}"; //Grid fängt bei 1,1 an...
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

    private bool CalcArea(int size)
    {
        bool continueCalc = false;

        for (int y = 0; y < _grid.SizeY - size; y++)
        {
            for (int x = 0; x < _grid.SizeX - size; x++)
            {
                var value = GetAreaValue(x, y, size);
                _areaSize.Add(new(x, y, size), value);

                if (value >= 0)
                {
                    continueCalc = true;
                }
            }
        }

        return continueCalc;
    }

    private int GetAreaValue(int x, int y, int size)
    {
        int areaSize;

        if (size > 2)
        {
            areaSize = _areaSize[new(x, y, size - 1)];
        }
        else
        {
            areaSize = _grid.GetValue(x, y);
        }

        for (int i = 0; i < size; i++)
        {
            areaSize += _grid.GetValue(x + i, y + size - 1);
        }
        for (int i = 0; i < size - 1; i++)
        {
            areaSize += _grid.GetValue(x + size - 1, y + i);
        }

        return areaSize;
    }

    private readonly struct Area(int x, int y, int size)
    {
        public int X { get; init; } = x;
        public int Y { get; init; } = y;
        public int Size { get; init; } = size;

        public override string ToString()
        {
            return $"{X},{Y},{Size}";
        }
    }
}
