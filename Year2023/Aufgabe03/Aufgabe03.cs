using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2023;

internal class Aufgabe03 : IAufgabe
{
    private readonly Grid _grid;
    public Aufgabe03()
    {
        _grid = Grid.CreateCharGrid(2023, 3);
    }

    public string Calc()
    {
        int result = 0;

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                if (!char.IsDigit(_grid.GetValue(x, y)))
                {
                    continue;
                }

                var length = GetLength(x, y);

                if (IsSymbolAround(x, x + length - 1, y))
                {
                    result += GetNumber(x, y, length);
                }

                x += length;
            }
        }

        return result.ToString();
    }

    private int GetLength(int x, int y)
    {
        bool isDigit;
        int length = 1;

        do
        {
            isDigit = false;
            x++;

            if (_grid.IsInBounds(x, y) && char.IsDigit(_grid.GetValue(x, y)))
            {
                isDigit = true;
                length++;
            }
        } while (isDigit);

        return length;
    }

    private bool IsSymbolAround(int xFrom, int xTo, int y)
    {
        xFrom--;
        xTo++;

        for (int x = xFrom; x <= xTo; x++)
        {
            if (IsSymbol(x, y - 1))
            {
                return true;
            }

            if (IsSymbol(x, y + 1))
            {
                return true;
            }
        }

        if (IsSymbol(xFrom, y))
        {
            return true;
        }

        if (IsSymbol(xTo, y))
        {
            return true;
        }

        return false;
    }

    private bool IsSymbol(int x, int y)
    {
        if (!_grid.IsInBounds(x, y))
        {
            return false;
        }

        return _grid.GetValue(x, y) != '.';
    }

    private int GetNumber(int x, int y, int length)
    {
        StringBuilder builder = new(length);
        for (int i = 0; i < length; i++)
        {
            builder.Append(_grid.GetValue(x + i, y));
        }

        return int.Parse(builder.ToString());
    }

}
