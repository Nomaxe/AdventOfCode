using AdventOfCode.Utils;
using AdventOfCode.Year2019.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2019;

internal class Aufgabe17b : IAufgabe
{
    private readonly IntCode _intcode;
    private readonly Grid _grid;
    private Point _position;
    private Direction _direction;

    public Aufgabe17b()
    {
        _intcode = new(2019, 17);
        _grid = new(50, 50);
    }

    public string Calc()
    {
        //In der Main-Funktion dürfen nur Methoden sein. Man kann das Coding also dafür noch sehr optimieren

        int x = 0;
        int y = 0;


        _intcode.Calc();

        foreach (var character in _intcode.Out)
        {
            if (character == IntCode.NewLineNumber)
            {
                x = 0;
                y++;
                continue;
            }

            _grid.SetValue(x, y, (char)character);
            x++;
        }

        var path = GetPath();
        var methodA = OptimizePath(path, "A");
        var methodB = OptimizePath(path, "B");
        var methodC = OptimizePath(path, "C");

        _intcode.Reset();
        _intcode.SetCode(0, 2);
        InputToIntcode(string.Join(',', path));
        InputToIntcode(methodA);
        InputToIntcode(methodB);
        InputToIntcode(methodC);
        _intcode.AddInput('n');
        _intcode.AddInputNewLine();
        _intcode.Calc();

        return _intcode.Out[^1].ToString();
    }

    private List<string> GetPath()
    {
        List<string> path = [$"{GetStart()},{GetLength()}"];

        do
        {
            var nextTurn = GetTurn();
            if (!nextTurn.HasValue)
            {
                return path;
            }

            var length = GetLength();

            path.Add($"{nextTurn.Value},{length}");
        } while (true);
    }

    private char GetStart()
    {
        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                var value = _grid.GetValue(x, y);

                if (value == '<')
                {
                    _position = new(x, y);
                    if (_grid.GetValue(x, y - 1) == '#')
                    {
                        _direction = Direction.Up;
                        return 'R';
                    }
                    else
                    {
                        _direction = Direction.Down;
                        return 'L';
                    }
                }

                if (value == '^')
                {
                    _position = new(x, y);
                    if (_grid.GetValue(x + 1, y) == '#')
                    {
                        _direction = Direction.Right;
                        return 'R';
                    }
                    else
                    {
                        _direction = Direction.Left;
                        return 'L';
                    }
                }

                if (value == '>')
                {
                    _position = new(x, y);
                    if (_grid.GetValue(x, y - 1) == '#')
                    {
                        _direction = Direction.Down;
                        return 'R';
                    }
                    else
                    {
                        _direction = Direction.Up;
                        return 'L';
                    }
                }

                if (value == 'v')
                {
                    _position = new(x, y);
                    if (_grid.GetValue(x - 1, y) == '#')
                    {
                        _direction = Direction.Left;
                        return 'R';
                    }
                    else
                    {
                        _direction = Direction.Right;
                        return 'L';
                    }
                }
            }
        }

        throw new NotImplementedException();
    }

    private char? GetTurn()
    {
        if (_direction == Direction.Up)
        {
            if (_grid.GetValue(_position.X - 1, _position.Y) == '#')
            {
                _direction = Direction.Left;
                return 'L';
            }
            else if (_grid.GetValue(_position.X + 1, _position.Y) == '#')
            {
                _direction = Direction.Right;
                return 'R';
            }
        }

        if (_direction == Direction.Right)
        {
            if (_grid.GetValue(_position.X, _position.Y - 1) == '#')
            {
                _direction = Direction.Up;
                return 'L';
            }
            else if (_grid.GetValue(_position.X, _position.Y + 1) == '#')
            {
                _direction = Direction.Down;
                return 'R';
            }
        }

        if (_direction == Direction.Down)
        {
            if (_grid.GetValue(_position.X + 1, _position.Y) == '#')
            {
                _direction = Direction.Right;
                return 'L';
            }
            else if (_grid.GetValue(_position.X - 1, _position.Y) == '#')
            {
                _direction = Direction.Left;
                return 'R';
            }
        }

        if (_direction == Direction.Left)
        {
            if (_grid.GetValue(_position.X, _position.Y + 1) == '#')
            {
                _direction = Direction.Down;
                return 'L';
            }
            else if (_grid.GetValue(_position.X, _position.Y - 1) == '#')
            {
                _direction = Direction.Up;
                return 'R';
            }
        }

        return null;
    }

    private int GetLength()
    {
        int length = -1;
        Point nextPoint = _position;

        do
        {
            _position = nextPoint;
            length++;
            nextPoint = _position.Move(_direction);
        } while (nextPoint.X >= 0 && nextPoint.Y >= 0 && _grid.GetValue(nextPoint) == '#');

        return length;
    }

    private static string OptimizePath(List<string> path, string methodName)
    {
        int bestResult = 0;
        int bestResultCount = 0;
        List<string> bestResultMethodImplementation = new();
        int[] indexes = [];

        for (int i = 5; i >= 2; i--)
        {
            for (int j = 0; j + i < path.Count; j++)
            {
                var check = path[j..(j + i)];

                if (check.Any(x => x.Length == 1))
                {
                    continue;
                }

                var existsAfter = DoesExistsAfter(path, check, j);

                if ((existsAfter.Count + 1) * i > (bestResult + 1) * bestResultCount)
                {
                    bestResult = existsAfter.Count;
                    bestResultCount = i;
                    indexes = new int[existsAfter.Count + 1];
                    indexes[0] = j;
                    existsAfter.CopyTo(indexes, 1);
                    bestResultMethodImplementation = check;
                }
            }
        }

        if (bestResult == 0)
        {
            throw new NotImplementedException();
        }

        for (int k = indexes.Length - 1; k >= 0; k--)
        {
            path.RemoveRange(indexes[k] + 1, bestResultCount - 1);
            path[indexes[k]] = methodName;
        }

        return string.Join(',', bestResultMethodImplementation);
    }

    private static List<int> DoesExistsAfter(List<string> path, List<string> check, int offset)
    {
        List<int> startIndexes = new();

        for (int i = offset + check.Count; i + check.Count <= path.Count; i++)
        {
            bool equal = true;

            for (int j = 0; j < check.Count; j++)
            {
                if (path[i + j] != check[j])
                {
                    equal = false;
                    break;
                }
            }

            if (equal)
            {
                startIndexes.Add(i);
            }
        }

        return startIndexes;
    }

    private void InputToIntcode(string input)
    {
        foreach (var character in input)
        {
            _intcode.AddInput((int)character);
        }

        _intcode.AddInput(10);
    }
}
