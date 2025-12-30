using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe08 : IAufgabe
{
    private readonly string[] _input;
    private readonly GridBool _grid;

    private const int SizeX = 50;
    private const int SizeY = 6;

    public Aufgabe08()
    {
        _input = Utilities.ReadInput(2016, 8);
        _grid = new(SizeX, SizeY);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            if (line.StartsWith("rect"))
            {
                var size = line[5..].Split('x').Select(int.Parse).ToArray();

                for (int y = 0; y < size[1]; y++)
                {
                    for (int x = 0; x < size[0]; x++)
                    {
                        _grid.SetValue(x, y, true);
                    }
                }
            }
            else if (line.StartsWith("rotate column"))
            {
                var values = new bool[SizeY];
                var numbers = line.GetUnsignedNumbers();

                for (int y = 0; y < SizeY; y++)
                {
                    values[y] = _grid.GetValue(numbers[0], y);
                }

                for (int y = 0; y < SizeY; y++)
                {
                    _grid.SetValue(numbers[0], (y + numbers[1]) % SizeY, values[y]);
                }
            }
            else if (line.StartsWith("rotate row"))
            {
                var values = new bool[SizeX];
                var numbers = line.GetUnsignedNumbers();

                for (int x = 0; x < SizeX; x++)
                {
                    values[x] = _grid.GetValue(x, numbers[0]);
                }

                for (int x = 0; x < SizeX; x++)
                {
                    _grid.SetValue((x + numbers[1]) % SizeX, numbers[0], values[x]);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return _grid.GetCountOfValue(true).ToString();
    }
}
