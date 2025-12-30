using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2023;

internal class Aufgabe03b : IAufgabe
{
    private readonly Grid _grid;
    public Aufgabe03b()
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
                if (_grid.GetValue(x, y) != '*')
                {
                    continue;
                }

                result += GetResult(x, y);
            }
        }

        return result.ToString();
    }

    public int GetResult(int x, int y)
    {
        var result = GetNumbers(x, y);

        if (result.Count > 1)
        {
            return result.Aggregate((x, y) => x * y);
        }

        return 0;
    }

    private List<int> GetNumbers(int x, int y)
    {
        List<int> numbers = new(2);
        bool numberFound = false;

        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            var value = _grid.GetValue(x + xOffset, y - 1);
            var isDigit = char.IsAsciiDigit(value);

            if (!isDigit)
            {
                numberFound = false;
            }
            else if (!numberFound && isDigit)
            {
                numbers.Add(GetNumber(x + xOffset, y - 1));
                numberFound = true;
            }
        }

        numberFound = false;
        for (int xOffset = -1; xOffset <= 1; xOffset++)
        {
            var value = _grid.GetValue(x + xOffset, y + 1);
            var isDigit = char.IsAsciiDigit(value);

            if (!isDigit)
            {
                numberFound = false;
            }
            else if (!numberFound && isDigit)
            {
                numbers.Add(GetNumber(x + xOffset, y + 1));
                numberFound = true;
            }
        }

        if (char.IsAsciiDigit(_grid.GetValue(x - 1, y)))
        {
            numbers.Add(GetNumber(x - 1, y));
        }
        if (char.IsAsciiDigit(_grid.GetValue(x + 1, y)))
        {
            numbers.Add(GetNumber(x + 1, y));
        }

        return numbers;
    }

    private int GetNumber(int x, int y)
    {
        int xFrom = x;
        int xTo = x;

        while (_grid.IsInBounds(xFrom - 1, y) && char.IsAsciiDigit(_grid.GetValue(xFrom - 1, y)))
        {
            xFrom--;
        }
        ;

        while (_grid.IsInBounds(xTo + 1, y) && char.IsAsciiDigit(_grid.GetValue(xTo + 1, y)))
        {
            xTo++;
        }
        ;

        StringBuilder builder = new(xTo - xFrom + 1);
        for (int i = xFrom; i <= xTo; i++)
        {
            builder.Append(_grid.GetValue(i, y));
        }

        return int.Parse(builder.ToString());
    }
}
