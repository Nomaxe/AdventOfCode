using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2024;

internal class Aufgabe12b : IAufgabe
{
    private readonly char[,] _input;
    private readonly char[,] _orginalInput;
    private char _currentChar;
    private readonly List<Point> _checkPoints = [];
    private List<PointChecked> _pointsOfPlant = [];

    private int _result = 0;

    private const char empty = '.';

    public Aufgabe12b()
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
                _pointsOfPlant.Add(new(_checkPoints[0]));
                _currentChar = _input[y, x];
                _input[y, x] = empty;

                do
                {
                    Check(_checkPoints[0]);
                    _checkPoints.RemoveAt(0);
                } while (_checkPoints.Count > 0);

                int fences = 0;
                _pointsOfPlant = _pointsOfPlant.OrderBy(x => x.Y).ThenBy(x => x.X).ToList();
                foreach (var point in _pointsOfPlant)
                {
                    if (GetCharacterOfOrginal(point.X, point.Y + 1) != _currentChar && !point.CheckedBottom)
                    {
                        fences++;
                        int nextX = point.X + 1;
                        int nextY = point.Y;
                        while (true)
                        {
                            var setChecked = _pointsOfPlant.Where(point => point.X == nextX && point.Y == nextY);
                            if (!setChecked.Any())
                            {
                                break;
                            }

                            var character = GetCharacterOfOrginal(nextX, nextY + 1);
                            if (character == _currentChar)
                            {
                                break;
                            }

                            setChecked.First().CheckedBottom = true;
                            nextX++;
                        }
                    }
                    point.CheckedBottom = true;

                    if (GetCharacterOfOrginal(point.X, point.Y - 1) != _currentChar && !point.CheckedTop)
                    {
                        fences++;
                        int nextX = point.X + 1;
                        int nextY = point.Y;
                        while (true)
                        {
                            var setChecked = _pointsOfPlant.Where(point => point.X == nextX && point.Y == nextY);
                            if (!setChecked.Any())
                            {
                                break;
                            }

                            var character = GetCharacterOfOrginal(nextX, nextY - 1);
                            if (character == _currentChar)
                            {
                                break;
                            }

                            setChecked.First().CheckedTop = true;
                            nextX++;
                        }
                    }
                    point.CheckedTop = true;

                    if (GetCharacterOfOrginal(point.X + 1, point.Y) != _currentChar && !point.CheckedRight)
                    {
                        fences++;
                        int nextX = point.X;
                        int nextY = point.Y + 1;
                        while (true)
                        {
                            var setChecked = _pointsOfPlant.Where(point => point.X == nextX && point.Y == nextY);
                            if (!setChecked.Any())
                            {
                                break;
                            }

                            var character = GetCharacterOfOrginal(nextX + 1, nextY);
                            if (character == _currentChar)
                            {
                                break;
                            }

                            setChecked.First().CheckedRight = true;
                            nextY++;
                        }
                    }
                    point.CheckedRight = true;

                    if (GetCharacterOfOrginal(point.X - 1, point.Y) != _currentChar && !point.CheckedLeft)
                    {
                        fences++;
                        int nextX = point.X;
                        int nextY = point.Y + 1;
                        while (true)
                        {
                            var setChecked = _pointsOfPlant.Where(point => point.X == nextX && point.Y == nextY);
                            if (!setChecked.Any())
                            {
                                break;
                            }

                            var character = GetCharacterOfOrginal(nextX - 1, nextY);
                            if (character == _currentChar)
                            {
                                break;
                            }

                            setChecked.First().CheckedLeft = true;
                            nextY++;
                        }
                    }
                    point.CheckedLeft = true;
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
        _pointsOfPlant.Add(new(_checkPoints[^1]));
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

    private class PointChecked
    {
        public Point Point { get; private init; }
        public bool CheckedLeft = false;
        public bool CheckedRight = false;
        public bool CheckedTop = false;
        public bool CheckedBottom = false;
        public int X => Point.X;
        public int Y => Point.Y;

        public PointChecked(Point point)
        {
            Point = point;
        }

        public override string ToString()
        {
            StringBuilder builder = new();
            builder.Append(Point.ToString());
            builder.Append(CheckedTop ? 'X' : ' ');
            builder.Append(',');
            builder.Append(CheckedRight ? 'X' : ' ');
            builder.Append(',');
            builder.Append(CheckedBottom ? 'X' : ' ');
            builder.Append(',');
            builder.Append(CheckedLeft ? 'X' : ' ');

            return builder.ToString();
        }
    }
}
