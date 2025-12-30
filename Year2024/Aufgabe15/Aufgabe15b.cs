using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe15b : IAufgabe
{
    private readonly char[,] _map;
    private readonly string _moves;

    private Point _currentPosition;

    public Aufgabe15b()
    {
        var input = Utilities.ReadInput(2024, 15);
        var whiteline = Array.IndexOf(input, string.Empty);
        _map = new char[whiteline, input[0].Length * 2];
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                switch (input[y][x])
                {
                    case '@':
                        _currentPosition = new(x * 2, y);
                        _map[y, x * 2] = '@';
                        _map[y, x * 2 + 1] = '.';
                        break;
                    case '.':
                        _map[y, x * 2] = '.';
                        _map[y, x * 2 + 1] = '.';
                        break;
                    case '#':
                        _map[y, x * 2] = '#';
                        _map[y, x * 2 + 1] = '#';
                        break;
                    case 'O':
                        _map[y, x * 2] = '[';
                        _map[y, x * 2 + 1] = ']';
                        break;
                }
                ;
            }
        }

        _moves = string.Empty;
        foreach (var line in input.Skip(whiteline + 1))
        {
            _moves += line;
        }
    }

    public string Calc()
    {
        foreach (var move in _moves)
        {
            var direction = GetDirection(move);
            var nextPosition = GetNextPosition(_currentPosition, direction);

            if (!IsMovePossible(nextPosition, direction))
            {
                continue;
            }

            SetChar(_currentPosition, direction);
            _currentPosition = nextPosition;
        }

        int result = 0;
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.GetLength(1); x++)
            {
                if (_map[y, x] != '[')
                {
                    continue;
                }

                result += y * 100 + x;
            }
        }

        return result.ToString();
    }

    private bool IsMovePossible(Point point, Direction direction, bool checkOtherHalf = true)
    {
        var nextChar = _map[point.Y, point.X];
        return nextChar switch
        {
            '.' => true,
            '#' => false,
            '[' => direction switch
            {
                Direction.Left or Direction.Right => IsMovePossible(GetNextPosition(point, direction), direction),
                Direction.Up or Direction.Down => IsMovePossible(GetNextPosition(point, direction), direction) && (!checkOtherHalf || IsMovePossible(GetNextPosition(point, Direction.Right), direction, false)),
                _ => throw new NotImplementedException(),
            },
            ']' => direction switch
            {
                Direction.Left or Direction.Right => IsMovePossible(GetNextPosition(point, direction), direction),
                Direction.Up or Direction.Down => IsMovePossible(GetNextPosition(point, direction), direction) && (!checkOtherHalf || IsMovePossible(GetNextPosition(point, Direction.Left), direction, false)),
                _ => throw new NotImplementedException(),
            },
            _ => throw new NotImplementedException()
        };
    }

    private static Point GetNextPosition(Point point, Direction direction)
    {
        return direction switch
        {
            Direction.Left => new(point.X - 1, point.Y),
            Direction.Right => new(point.X + 1, point.Y),
            Direction.Up => new(point.X, point.Y - 1),
            Direction.Down => new(point.X, point.Y + 1),
            _ => throw new NotImplementedException(),
        };
    }

    private void SetChar(Point currentPoint, Direction direction, bool checkOtherHalf = true)
    {
        var characterAtCurrentPosition = _map[currentPoint.Y, currentPoint.X];
        var nextPoint = GetNextPosition(currentPoint, direction);
        var characterAtNextPosition = _map[nextPoint.Y, nextPoint.X];

        if (characterAtNextPosition == '[' || characterAtNextPosition == ']')
        {
            SetChar(nextPoint, direction);
        }

        switch (characterAtCurrentPosition)
        {
            case '.':
            case '@':
                _map[nextPoint.Y, nextPoint.X] = characterAtCurrentPosition;
                _map[currentPoint.Y, currentPoint.X] = '.';
                break;
            case '[':
                _map[nextPoint.Y, nextPoint.X] = characterAtCurrentPosition;
                _map[currentPoint.Y, currentPoint.X] = '.';
                if (checkOtherHalf && (direction == Direction.Up || direction == Direction.Down))
                {
                    SetChar(GetNextPosition(currentPoint, Direction.Right), direction, false);
                }
                break;
            case ']':
                _map[nextPoint.Y, nextPoint.X] = characterAtCurrentPosition;
                _map[currentPoint.Y, currentPoint.X] = '.';
                if (checkOtherHalf && (direction == Direction.Up || direction == Direction.Down))
                {
                    SetChar(GetNextPosition(currentPoint, Direction.Left), direction, false);
                }
                break;
            default:
                throw new NotImplementedException();
        }
    }

    private static Direction GetDirection(char character)
    {
        return character switch
        {
            '<' => Direction.Left,
            '>' => Direction.Right,
            '^' => Direction.Up,
            'v' => Direction.Down,
            _ => throw new NotImplementedException()
        };
    }

    private enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }
}
