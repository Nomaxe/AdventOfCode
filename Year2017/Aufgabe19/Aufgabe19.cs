using AdventOfCode.Utils;
using System.Text;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2017;

internal class Aufgabe19 : IAufgabe
{
    private readonly Grid _grid;
    private Direction _direction;

    public Aufgabe19()
    {
        _grid = Grid.CreateCharGrid(2017, 19);
        _direction = Direction.Down;
    }

    public string Calc()
    {
        Point currentPoint = GetStartPoint();
        var currentValue = _grid.GetValue(currentPoint);
        StringBuilder builder = new();

        while (currentValue != ' ')
        {
            if (char.IsAsciiLetterUpper(currentValue))
            {
                builder.Append(currentValue);
            }
            else if (currentValue == '+')
            {
                switch (_direction)
                {
                    case Direction.Up:
                    case Direction.Down:
                        Point checkPoint = new(currentPoint.X + 1, currentPoint.Y);
                        _direction = _grid.GetValue(checkPoint) == '-' ? Direction.Right : Direction.Left;
                        break;
                    case Direction.Left:
                    case Direction.Right:
                        checkPoint = new(currentPoint.X, currentPoint.Y + 1);
                        _direction = _grid.GetValue(checkPoint) == '|' ? Direction.Down : Direction.Up;
                        break;
                }
            }

            currentPoint = currentPoint.Move(_direction);
            currentValue = _grid.GetValue(currentPoint);
        }

        return builder.ToString();
    }

    private Point GetStartPoint()
    {
        for (int x = 0; x < _grid.SizeX; x++)
        {
            if (_grid.GetValue(x, 0) != ' ')
            {
                return new(x, 0);
            }
        }

        throw new NotImplementedException();
    }
}
