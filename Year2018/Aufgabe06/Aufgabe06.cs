using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe06 : IAufgabe
{
    private readonly string[] _input;
    private readonly Point[] _startPoints;
    private readonly Grid _grid;

    private const int GridSize = 400;

    public Aufgabe06()
    {
        _input = Utilities.ReadInput(2018, 6);
        _startPoints = new Point[_input.Length];
        _grid = new(GridSize, GridSize, ' ');
    }

    public string Calc()
    {
        char currentChar = 'A';
        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();
            _grid.SetValue(numbers[0], numbers[1], currentChar);
            _startPoints[currentChar - 'A'] = new(numbers[0], numbers[1]);
            currentChar++;
        }

        for (int y = 0; y < GridSize; y++)
        {
            for (int x = 0; x < GridSize; x++)
            {
                if (_grid.GetValue(x, y) != ' ')
                {
                    continue;
                }

                int minLength = int.MaxValue;
                currentChar = ' ';
                Point point = new(x, y);

                foreach (var startPoint in _startPoints)
                {
                    var length = point.GetManhattenDistance(startPoint);
                    if (length < minLength)
                    {
                        minLength = length;
                        currentChar = _grid.GetValue(startPoint);
                    }
                    else if (length == minLength)
                    {
                        currentChar = ' ';
                    }

                    _grid.SetValue(x, y, currentChar);
                }
            }
        }


        currentChar = 'A';
        int maxSize = 0;
        foreach (var line in _input)
        {
            if (!IsInfinite(currentChar))
            {
                var amount = _grid.GetCountOfValue(currentChar);
                maxSize = int.Max(maxSize, amount);
            }

            currentChar++;
        }

        return maxSize.ToString();
    }

    private bool IsInfinite(char character)
    {
        for (int i = 0; i < GridSize; i++)
        {
            if (_grid.GetValue(0, i) == character)
            {
                return true;
            }

            if (_grid.GetValue(i, 0) == character)
            {
                return true;
            }

            if (_grid.GetValue(GridSize - 1, i) == character)
            {
                return true;
            }

            if (_grid.GetValue(i, GridSize - 1) == character)
            {
                return true;
            }
        }

        return false;
    }
}
