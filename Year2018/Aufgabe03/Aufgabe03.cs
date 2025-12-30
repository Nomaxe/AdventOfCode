using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe03 : IAufgabe
{
    private readonly GridInt _grid;
    private readonly string[] _input;

    public Aufgabe03()
    {
        _input = Utilities.ReadInput(2018, 3);
        _grid = new(1000);
    }

    public string Calc()
    {
        HashSet<Point> points = [];

        foreach (var line in _input)
        {
            var numbers = line.GetNumbers();

            for (int y = numbers[2]; y < numbers[2] + numbers[4]; y++)
            {
                for (int x = numbers[1]; x < numbers[1] + numbers[3]; x++)
                {
                    if (_grid.GetValue(x, y) == 0)
                    {
                        _grid.SetValue(x, y, numbers[0]);
                    }
                    else
                    {
                        points.Add(new(x, y));
                    }
                }
            }
        }

        return points.Count.ToString();
    }
}
