using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe12 : IAufgabe
{
    private readonly char[,] _input;
    private readonly char[,] _orginalInput;
    private char _currentChar;
    private readonly List<Point> _checkPoints = [];
    private readonly List<Point> _pointsOfPlant = [];

    private int _result = 0;

    private const char empty = '.';

    public Aufgabe12()
    {
        var input = Utilities.ReadInput(2024, 12);
        _input = new char[input.Length, input[0].Length];
        for (int y = 0; y < _input.GetLength(0); y++)
        {
            for (int x = 0; x < _input.GetLength(1); x++)
            {
                _input[y, x] = input[y][x];
            }
        }

        _orginalInput = new char[_input.GetLength(0), _input.GetLength(1)];
        Array.Copy(_input, _orginalInput, _input.Length);
    }

    public string Calc()
    {
        for (int y = 0; y < _input.GetLength(0); y++)
        {

            for (int x = 0; x < _input.GetLength(1); x++)
            {
                if (_input[y, x] == empty)
                {
                    continue;
                }

                _checkPoints.Add(new(x, y));
                _pointsOfPlant.Clear();
                _pointsOfPlant.Add(_checkPoints[0]);
                _currentChar = _input[y, x];
                _input[y, x] = empty;

                do
                {
                    Check(_checkPoints[0]);
                    _checkPoints.RemoveAt(0);
                } while (_checkPoints.Count > 0);

                int fences = 0;
                foreach (var point in _pointsOfPlant)
                {
                    if (GetCharacterOfOrginal(point.X, point.Y + 1) != _currentChar)
                    {
                        fences++;
                    }
                    if (GetCharacterOfOrginal(point.X, point.Y - 1) != _currentChar)
                    {
                        fences++;
                    }
                    if (GetCharacterOfOrginal(point.X + 1, point.Y) != _currentChar)
                    {
                        fences++;
                    }
                    if (GetCharacterOfOrginal(point.X - 1, point.Y) != _currentChar)
                    {
                        fences++;
                    }
                }

                var cost = _pointsOfPlant.Count * fences;

                _result += cost;
            }
        }

        return _result.ToString();
    }

    private void Check(Point point)
    {
        CheckInner(point.X, point.Y + 1);
        CheckInner(point.X, point.Y - 1);
        CheckInner(point.X + 1, point.Y);
        CheckInner(point.X - 1, point.Y);
    }

    private void CheckInner(int x, int y)
    {
        if (GetCharacter(x, y) != _currentChar)
        {
            return;
        }

        _checkPoints.Add(new(x, y));
        _pointsOfPlant.Add(_checkPoints[^1]);
        _input[y, x] = empty;
    }

    private char GetCharacter(int x, int y)
    {
        if (x < 0 || x >= _input.GetLength(1))
        {
            return empty;
        }

        if (y < 0 || y >= _input.GetLength(0))
        {
            return empty;
        }

        return _input[y, x];
    }

    private char GetCharacterOfOrginal(int x, int y)
    {
        if (x < 0 || x >= _orginalInput.GetLength(1))
        {
            return empty;
        }

        if (y < 0 || y >= _orginalInput.GetLength(0))
        {
            return empty;
        }

        return _orginalInput[y, x];
    }
}
