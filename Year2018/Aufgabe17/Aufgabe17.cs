using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe17 : IAufgabe
{
    private readonly string[] _input;
    private readonly Grid _grid;
    private readonly Queue<Point> _queue;
    private int _smallestY;
    private int _biggestY;

    public Aufgabe17()
    {
        _input = Utilities.ReadInput(2018, 17);
        _grid = new(1000, 2000);
        _queue = new();
        _smallestY = int.MaxValue;
        _biggestY = 0;
    }

    public string Calc()
    {
        FillGrid();

        _queue.Enqueue(new(500, 0));
        _grid.SetValue(500, 0, '|');

        do
        {
            var point = _queue.Dequeue();
            point = MoveDown(point);

            if (point.Y > _biggestY)
            {
                continue;
            }
            FillPool(point);
        } while (_queue.Count > 0);

        int count = 0;
        for (int y = _smallestY; y <= _biggestY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                var value = _grid.GetValue(x, y);
                if (value == '|' || value == '~')
                {
                    count++;
                }
            }
        }

        return count.ToString();
    }

    private Point MoveDown(Point point)
    {
        while (point.Y < 1998 && _grid.GetValue(point.X, point.Y + 1) == '\0' || _grid.GetValue(point.X, point.Y + 1) == '|')
        {
            point = new(point.X, point.Y + 1);
            _grid.SetValue(point, '|');
        }

        if (point.Y < 1999 && _grid.GetValue(point.X, point.Y + 1) == '~')
        {
            point = new(point.X, point.Y + 1);
        }

        return point;
    }

    private void FillPool(Point point)
    {
        bool overlapping = false;

        do
        {
            int xFrom = point.X;
            int xTo = point.X;

            _grid.SetValue(point, '|');
            while (_grid.GetValue(xFrom - 1, point.Y) != '#')
            {
                xFrom--;
                _grid.SetValue(xFrom, point.Y, '|');

                if (_grid.GetValue(xFrom, point.Y + 1) == '\0')
                {
                    overlapping = true;
                    _queue.Enqueue(new(xFrom, point.Y));
                    break;
                }
                else if (_grid.GetValue(xFrom, point.Y + 1) == '|')
                {
                    overlapping = true;
                    break;
                }
            }

            while (_grid.GetValue(xTo + 1, point.Y) != '#')
            {
                xTo++;
                _grid.SetValue(xTo, point.Y, '|');

                if (_grid.GetValue(xTo, point.Y + 1) == '\0')
                {
                    overlapping = true;
                    _queue.Enqueue(new(xTo, point.Y));
                    break;
                }
                else if (_grid.GetValue(xTo, point.Y + 1) == '|')
                {
                    overlapping = true;
                    break;
                }
            }

            if (!overlapping)
            {
                for (int x = xFrom; x <= xTo; x++)
                {
                    _grid.SetValue(x, point.Y, '~');
                }
            }
            point = new(point.X, point.Y - 1);
        } while (!overlapping);
    }

    private void FillGrid()
    {
        foreach (var line in _input)
        {
            var numbers = line.GetNumbers();

            if (line[0] == 'x')
            {
                for (int i = numbers[1]; i <= numbers[2]; i++)
                {
                    _grid.SetValue(numbers[0], i, '#');
                }

                if (numbers[1] < _smallestY)
                {
                    _smallestY = numbers[1];
                }
                if (numbers[2] > _biggestY)
                {
                    _biggestY = numbers[2];
                }
            }
            else
            {
                for (int i = numbers[1]; i <= numbers[2]; i++)
                {
                    _grid.SetValue(i, numbers[0], '#');
                }

                if (numbers[0] < _smallestY)
                {
                    _smallestY = numbers[0];
                }
                if (numbers[0] > _biggestY)
                {
                    _biggestY = numbers[0];
                }
            }
        }
    }

    private void Draw()
    {
        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 300; x < _grid.SizeX; x++)
            {
                var value = _grid.GetValue(x, y);

                if (value == '\0')
                {
                    Console.Write(' ');
                }
                else
                {
                    Console.Write(value);
                }
            }

            Console.WriteLine();
        }
    }
}
