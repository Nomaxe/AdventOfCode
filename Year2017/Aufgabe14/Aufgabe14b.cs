using AdventOfCode.Utils;
using AdventOfCode.Year2017.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe14b : IAufgabe
{
    private readonly string _input;
    private readonly HashSet<Point> _pointsToCheck;

    private const int GridSize = 128;

    public Aufgabe14b()
    {
        _input = Utilities.ReadInputAsString(2017, 14);
        _pointsToCheck = [];
    }

    public string Calc()
    {
        for (int y = 0; y < 128; y++)
        {
            KnotHash hash = new($"{_input}-{y}");
            hash.Calc();
            var result = hash.GetResult();

            int x = 0;
            foreach (var character in result)
            {
                var number = ToHexDecimal(character);
                if ((number & 8) >= 1)
                {
                    _pointsToCheck.Add(new(x, y));
                }
                if ((number & 4) >= 1)
                {
                    _pointsToCheck.Add(new(x + 1, y));
                }
                if ((number & 2) >= 1)
                {
                    _pointsToCheck.Add(new(x + 2, y));
                }
                if ((number & 1) >= 1)
                {
                    _pointsToCheck.Add(new(x + 3, y));
                }

                x += 4;
            }
        }

        int groupCount = 0;

        while (_pointsToCheck.Count > 0)
        {
            CalcGroup(_pointsToCheck.First());
            groupCount++;
        }

        return groupCount.ToString();
    }

    private void CalcGroup(Point start)
    {
        Queue<Point> queue = new();
        queue.Enqueue(start);
        _pointsToCheck.Remove(start);

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();
            var neighbours = point.GetNeighbours().Where(x => x.X >= 0 && x.Y >= 0 && x.X < GridSize && x.Y < GridSize)
                                                  .Where(_pointsToCheck.Contains);
            foreach (var neighbour in neighbours)
            {
                queue.Enqueue(neighbour);
                _pointsToCheck.Remove(neighbour);
            }
        }
    }

    private static int ToHexDecimal(char character)
    {
        if (character <= '9')
        {
            return character - '0';
        }

        return character - 'a' + 10;
    }
}
