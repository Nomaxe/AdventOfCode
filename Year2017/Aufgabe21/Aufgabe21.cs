using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe21 : IAufgabe
{
    private readonly Dictionary<string, string> _patterns;
    private Grid _grid;

    public Aufgabe21()
    {
        var input = Utilities.ReadInput(2017, 21);
        _patterns = new(input.Length);
        foreach (var line in input)
        {
            var split = line.Split(" => ");
            _patterns.Add(split[0], split[1]);
        }

        _grid = new(3);
        _grid.SetValue(0, 0, '.');
        _grid.SetValue(1, 0, '#');
        _grid.SetValue(2, 0, '.');
        _grid.SetValue(0, 1, '.');
        _grid.SetValue(1, 1, '.');
        _grid.SetValue(2, 1, '#');
        _grid.SetValue(0, 2, '#');
        _grid.SetValue(1, 2, '#');
        _grid.SetValue(2, 2, '#');
    }

    public string Calc()
    {
        for (int i = 0; i < 5; i++)
        {
            var squareSize = _grid.SizeX % 2 == 0 ? 2 : 3;
            Grid nextGrid;

            if (squareSize == 2)
            {
                nextGrid = new(_grid.SizeX / 2 * 3);
                for (int y = 0; y < _grid.SizeY; y += squareSize)
                {
                    for (int x = 0; x < _grid.SizeX; x += squareSize)
                    {
                        FillNextPattern2(nextGrid, GetNextPattern2(GetCurrentPattern2(x, y)), x, y);
                    }
                }
            }
            else
            {
                nextGrid = new(_grid.SizeX / 3 * 4);
                for (int y = 0; y < _grid.SizeY; y += squareSize)
                {
                    for (int x = 0; x < _grid.SizeX; x += squareSize)
                    {
                        FillNextPattern3(nextGrid, GetNextPattern3(GetCurrentPattern3(x, y)), x, y);
                    }
                }
            }

            _grid = nextGrid;
        }

        return _grid.GetCountOfValue('#').ToString();
    }

    private string GetCurrentPattern2(int x, int y)
    {
        return $"{_grid.GetValue(x, y)}{_grid.GetValue(x + 1, y)}/{_grid.GetValue(x, y + 1)}{_grid.GetValue(x + 1, y + 1)}";
    }

    private string GetCurrentPattern3(int x, int y)
    {
        return $"{_grid.GetValue(x, y)}{_grid.GetValue(x + 1, y)}{_grid.GetValue(x + 2, y)}/" +
               $"{_grid.GetValue(x, y + 1)}{_grid.GetValue(x + 1, y + 1)}{_grid.GetValue(x + 2, y + 1)}/" +
               $"{_grid.GetValue(x, y + 2)}{_grid.GetValue(x + 1, y + 2)}{_grid.GetValue(x + 2, y + 2)}";
    }

    private string GetNextPattern2(string pattern)
    {
        if (_patterns.TryGetValue(pattern, out var result))
        {
            return result;
        }

        var checkPattern = $"{pattern[3]}{pattern[0]}/{pattern[4]}{pattern[1]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[4]}{pattern[3]}/{pattern[1]}{pattern[0]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[1]}{pattern[4]}/{pattern[0]}{pattern[3]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[1]}{pattern[0]}/{pattern[4]}{pattern[3]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[4]}{pattern[1]}/{pattern[3]}{pattern[0]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[3]}{pattern[4]}/{pattern[0]}{pattern[1]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[0]}{pattern[3]}/{pattern[1]}{pattern[4]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        throw new NotImplementedException();
    }

    private string GetNextPattern3(string pattern)
    {
        if (_patterns.TryGetValue(pattern, out var result))
        {
            return result;
        }

        var checkPattern = $"{pattern[8]}{pattern[4]}{pattern[0]}/{pattern[9]}{pattern[5]}{pattern[1]}/{pattern[10]}{pattern[6]}{pattern[2]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[10]}{pattern[9]}{pattern[8]}/{pattern[6]}{pattern[5]}{pattern[4]}/{pattern[2]}{pattern[1]}{pattern[0]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[2]}{pattern[6]}{pattern[10]}/{pattern[1]}{pattern[5]}{pattern[9]}/{pattern[0]}{pattern[4]}{pattern[8]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[2]}{pattern[1]}{pattern[0]}/{pattern[6]}{pattern[5]}{pattern[4]}/{pattern[10]}{pattern[9]}{pattern[8]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[10]}{pattern[6]}{pattern[2]}/{pattern[9]}{pattern[5]}{pattern[1]}/{pattern[8]}{pattern[4]}{pattern[0]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[8]}{pattern[9]}{pattern[10]}/{pattern[4]}{pattern[5]}{pattern[6]}/{pattern[0]}{pattern[1]}{pattern[2]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        checkPattern = $"{pattern[0]}{pattern[4]}{pattern[8]}/{pattern[1]}{pattern[5]}{pattern[9]}/{pattern[2]}{pattern[6]}{pattern[10]}";
        if (_patterns.TryGetValue(checkPattern, out result))
        {
            _patterns.Add(pattern, result);
            return result;
        }

        throw new NotImplementedException();
    }

    private static void FillNextPattern2(Grid grid, string pattern, int xOld, int yOld)
    {
        var x = xOld / 2 * 3;
        var y = yOld / 2 * 3;

        grid.SetValue(x, y, pattern[0]);
        grid.SetValue(x + 1, y, pattern[1]);
        grid.SetValue(x + 2, y, pattern[2]);
        grid.SetValue(x, y + 1, pattern[4]);
        grid.SetValue(x + 1, y + 1, pattern[5]);
        grid.SetValue(x + 2, y + 1, pattern[6]);
        grid.SetValue(x, y + 2, pattern[8]);
        grid.SetValue(x + 1, y + 2, pattern[9]);
        grid.SetValue(x + 2, y + 2, pattern[10]);
    }

    private static void FillNextPattern3(Grid grid, string pattern, int xOld, int yOld)
    {
        var x = xOld / 3 * 4;
        var y = yOld / 3 * 4;

        grid.SetValue(x, y, pattern[0]);
        grid.SetValue(x + 1, y, pattern[1]);
        grid.SetValue(x + 2, y, pattern[2]);
        grid.SetValue(x + 3, y, pattern[3]);
        grid.SetValue(x, y + 1, pattern[5]);
        grid.SetValue(x + 1, y + 1, pattern[6]);
        grid.SetValue(x + 2, y + 1, pattern[7]);
        grid.SetValue(x + 3, y + 1, pattern[8]);
        grid.SetValue(x, y + 2, pattern[10]);
        grid.SetValue(x + 1, y + 2, pattern[11]);
        grid.SetValue(x + 2, y + 2, pattern[12]);
        grid.SetValue(x + 3, y + 2, pattern[13]);
        grid.SetValue(x, y + 3, pattern[15]);
        grid.SetValue(x + 1, y + 3, pattern[16]);
        grid.SetValue(x + 2, y + 3, pattern[17]);
        grid.SetValue(x + 3, y + 3, pattern[18]);
    }
}
