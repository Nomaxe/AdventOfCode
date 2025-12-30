using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe09 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe09()
    {
        _input = Utilities.ReadInput(2025, 9);
    }

    public string Calc()
    {
        List<Point> points = new(_input.Length);
        foreach (var line in _input)
        {
            points.Add(new(line));
        }

        long size = 0;
        for (int i = 0; i < points.Count; i++)
        {
            for (int j = i + 1; j < points.Count; j++)
            {
                var point1 = points[i];
                var point2 = points[j];

                var checkSize = (long.Max(point1.X, point2.X) - long.Min(point1.X, point2.X) + 1) * (long.Max(point1.Y, point2.Y) - long.Min(point1.Y, point2.Y) + 1);

                if (checkSize > size)
                {
                    size = checkSize;
                }
            }
        }

        return size.ToString();
    }
}
