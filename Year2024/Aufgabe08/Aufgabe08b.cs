using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe08b : IAufgabe
{
    private readonly char[,] _input;
    private readonly char[,] _antinodes;
    private ulong _result;

    public Aufgabe08b()
    {
        var input = Utilities.ReadInput(2024, 8);
        _input = new char[input.Length, input[0].Length];
        _antinodes = new char[input.Length, input[0].Length];

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                _input[y, x] = input[y][x];
                _antinodes[y, x] = '.';
            }
        }
    }

    public string Calc()
    {
        for (int y = 0; y < _input.GetLength(0); y++)
        {
            for (int x = 0; x < _input.GetLength(1); x++)
            {
                if (_input[y, x] == '.')
                {
                    continue;
                }
                FindRemaining(x, y, _input[y, x]);
            }
        }

        return _result.ToString();
    }

    private void FindRemaining(int xCheck, int yCheck, char character)
    {
        for (int y = yCheck + 1; y < _input.GetLength(0); y++)
        {
            for (int x = 0; x < _input.GetLength(1); x++)
            {
                if (_input[y, x] == character)
                {
                    SetAntinode(x, y, character);
                    SetAntinode(xCheck, yCheck, character);

                    int xDifference = x - xCheck;
                    int yDifference = y - yCheck;
                    int xDifferenceLoop = xDifference;
                    int yDifferenceLoop = yDifference;
                    bool result;
                    do
                    {
                        result = SetAntinode(xCheck - xDifferenceLoop, yCheck - yDifferenceLoop, character);
                        xDifferenceLoop += xDifference;
                        yDifferenceLoop += yDifference;
                    } while (result);

                    xDifferenceLoop = xDifference;
                    yDifferenceLoop = yDifference;
                    do
                    {
                        result = SetAntinode(x + xDifferenceLoop, y + yDifferenceLoop, character);
                        xDifferenceLoop += xDifference;
                        yDifferenceLoop += yDifference;
                    } while (result);
                }
            }
        }
    }

    private bool SetAntinode(int x, int y, char character)
    {
        if (x < 0 || x >= _input.GetLength(1))
        {
            return false;
        }

        if (y < 0 || y >= _input.GetLength(0))
        {
            return false;
        }

        if (_antinodes[y, x] != '.')
        {
            return true;
        }

        _antinodes[y, x] = character;
        _result++;

        return true;
    }
}
