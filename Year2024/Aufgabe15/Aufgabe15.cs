using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe15 : IAufgabe
{
    private readonly string[] _input;
    private char[,] _map;
    private string _moves;

    private Point _currentPosition;

#pragma warning disable CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.
    public Aufgabe15()
    {
        _input = Utilities.ReadInput(2024, 15);
    }
#pragma warning restore CS8618 // Ein Non-Nullable-Feld muss beim Beenden des Konstruktors einen Wert ungleich NULL enthalten. Fügen Sie ggf. den „erforderlichen“ Modifizierer hinzu, oder deklarieren Sie den Modifizierer als NULL-Werte zulassend.

    public string Calc()
    {
        var whiteline = Array.IndexOf(_input, string.Empty);
        _map = new char[whiteline, _input[0].Length];
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.GetLength(1); x++)
            {
                _map[y, x] = _input[y][x];

                if (_map[y, x] == '@')
                {
                    _currentPosition = new(x, y);
                }
            }
        }

        _moves = string.Empty;
        foreach (var line in _input.Skip(whiteline + 1))
        {
            _moves += line;
        }

        foreach (var move in _moves)
        {
            var direction = GetDirection(move);
            var nextPosition = GetNextPosition(_currentPosition, direction);

            var moveAmount = IsMovePossible(nextPosition, direction, 1);
            if (moveAmount == 0)
            {
                continue;
            }

            _map[_currentPosition.Y, _currentPosition.X] = '.';
            _map[nextPosition.Y, nextPosition.X] = '@';
            _currentPosition = nextPosition;

            for (int i = 1; i < moveAmount; i++)
            {
                nextPosition = GetNextPosition(nextPosition, direction);
                _map[nextPosition.Y, nextPosition.X] = 'O';
            }
        }

        int result = 0;
        for (int y = 0; y < _map.GetLength(0); y++)
        {
            for (int x = 0; x < _map.GetLength(1); x++)
            {
                if (_map[y, x] != 'O')
                {
                    continue;
                }

                result += y * 100 + x;
            }
        }

        return result.ToString();
    }

    private int IsMovePossible(Point point, Direction direction, int amount)
    {
        var nextChar = _map[point.Y, point.X];
        return nextChar switch
        {
            '.' => amount,
            '#' => 0,
            'O' => IsMovePossible(GetNextPosition(point, direction), direction, amount + 1),
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
