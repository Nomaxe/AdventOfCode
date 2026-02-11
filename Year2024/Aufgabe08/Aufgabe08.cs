using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe08 : IAufgabe
{
    private readonly char[,] _grid;
    private readonly char[,] _antinodes;
    private ulong _result;

    public Aufgabe08()
    {
        var input = Utilities.ReadInput(2024, 8);
        _grid = new char[input.Length, input[0].Length];
        _antinodes = new char[input.Length, input[0].Length];

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                _grid[y, x] = input[y][x];
                _antinodes[y, x] = '.';
            }
        }
    }

    public string Calc()
    {
        for (int y = 0; y < _grid.GetLength(0); y++)
        {
            for (int x = 0; x < _grid.GetLength(1); x++)
            {
                if (_grid[y, x] == '.')
                {
                    continue;
                }

                FindHorizontal(x, y, _grid[y, x]);
                FindRemaining(x, y, _grid[y, x]);
            }
        }

        return _result.ToString();
    }

    private void FindHorizontal(int xCheck, int yCheck, char character)
    {
        for (int x = xCheck + 1; x < _grid.GetLength(1); x++)
        {
            if (_grid[yCheck, x] == character)
            {
                SetAntinode(xCheck + x, yCheck, character);
                SetAntinode(xCheck - x, yCheck, character);
            }
        }
    }

    private void FindRemaining(int xCheck, int yCheck, char character)
    {
        for (int y = yCheck + 1; y < _grid.GetLength(0); y++)
        {
            for (int x = 0; x < _grid.GetLength(1); x++)
            {
                if (_grid[y, x] == character)
                {
                    int xDifference = x - xCheck;
                    int yDifference = y - yCheck;
                    SetAntinode(xCheck - xDifference, yCheck - yDifference, character);
                    SetAntinode(x + xDifference, y + yDifference, character);
                }
            }
        }
    }

    private void SetAntinode(int x, int y, char character)
    {
        if (x < 0 || x >= _grid.GetLength(1))
        {
            return;
        }

        if (y < 0 || y >= _grid.GetLength(0))
        {
            return;
        }

        if (_antinodes[y, x] != '.')
        {
            return;
        }

        _antinodes[y, x] = character;
        _result++;
    }
}
