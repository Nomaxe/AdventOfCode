using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe10 : IAufgabe
{
    private readonly int[,] _input;
    private readonly List<(int XStart, int YStart, int XEnd, int YEnd)> _paths = [];
    private ulong _result;

    public Aufgabe10()
    {
        var input = Utilities.ReadInput(2024, 10);
        _input = new int[input.Length, input[0].Length];
        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                _input[y, x] = GetNumericValue(input[y][x]);
            }
        }
    }

    public string Calc()
    {
        for (int y = 0; y < _input.GetLength(0); y++)
        {
            for (int x = 0; x < _input.GetLength(1); x++)
            {
                if (_input[y, x] == 0)
                {
                    Check(x, y, 1, x, y);
                }
            }
        }

        return _result.ToString();
    }

    private void Check(int x, int y, int number, int xStart, int yStart)
    {
        CheckInner(x + 1, y, number, xStart, yStart);
        CheckInner(x - 1, y, number, xStart, yStart);
        CheckInner(x, y + 1, number, xStart, yStart);
        CheckInner(x, y - 1, number, xStart, yStart);
    }

    private void CheckInner(int x, int y, int number, int xStart, int yStart)
    {
        int numberAtPosition = GetNumberAtPosition(x, y);

        if (numberAtPosition != number)
        {
            return;
        }

        if (number == 9)
        {
            if (_paths.Any(path => path.XStart == xStart && path.YStart == yStart && path.XEnd == x && path.YEnd == y))
            {
                return;
            }

            _result++;
            _paths.Add(new(xStart, yStart, x, y));
            return;
        }

        Check(x, y, number + 1, xStart, yStart);
    }

    private int GetNumberAtPosition(int x, int y)
    {
        if (x < 0 || x >= _input.GetLength(1))
        {
            return -1;
        }

        if (y < 0 || y >= _input.GetLength(0))
        {
            return -1;
        }

        return _input[y, x];
    }

    private static int GetNumericValue(char character)
    {
        return Convert.ToInt32(char.GetNumericValue(character));
    }
}
