using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe04b : IAufgabe
{
    private readonly List<int> _numbers;
    private List<(GridInt Numbers, GridBool Drawn)> _grids;

    public Aufgabe04b()
    {
        var input = Utilities.ReadInput(2021, 4);
        _numbers = input[0].ToIntList();
        _grids = [];

        for (int i = 2; i < input.Length; i += 6)
        {
            var grid = input[i..(i + 5)];
            _grids.Add((CreateGrid(grid), new GridBool(5)));
        }
    }

    public string Calc()
    {
        for (int i = 0; i < _numbers.Count; i++)
        {
            List<(Grid<int> Numbers, GridBool Drawn)> nextGrids = new(_grids.Count);

            foreach (var grid in _grids)
            {
                var position = grid.Numbers.GetPointOfValueOrNull(_numbers[i]);
                if (position.HasValue)
                {
                    grid.Drawn.SetValue(position.Value, true);

                    if (i >= 5)
                    {
                        if (IsWinner(grid.Drawn))
                        {
                            if (_grids.Count > 1)
                            {
                                continue;
                            }
                            else
                            {
                                return (GetScoreOfGrid(grid.Numbers, grid.Drawn) * _numbers[i]).ToString();
                            }
                        }
                    }
                }

                nextGrids.Add(grid);
            }

            _grids = nextGrids;
        }

        throw new NotImplementedException();
    }

    private static GridInt CreateGrid(string[] input)
    {
        GridInt grid = new(5);

        for (int y = 0; y < 5; y++)
        {
            grid.SetValue(0, y, int.Parse(input[y][0..2]));
            grid.SetValue(1, y, int.Parse(input[y][3..5]));
            grid.SetValue(2, y, int.Parse(input[y][6..8]));
            grid.SetValue(3, y, int.Parse(input[y][9..11]));
            grid.SetValue(4, y, int.Parse(input[y][12..14]));
        }

        return grid;
    }

    private static bool IsWinner(GridBool grid)
    {
        for (int i = 0; i < 5; i++)
        {
            if (grid.GetValue(i, 0) && grid.GetValue(i, 1) && grid.GetValue(i, 2) && grid.GetValue(i, 3) && grid.GetValue(i, 4))
            {
                return true;
            }

            if (grid.GetValue(0, i) && grid.GetValue(1, i) && grid.GetValue(2, i) && grid.GetValue(3, i) && grid.GetValue(4, i))
            {
                return true;
            }
        }

        return false;
    }

    private static int GetScoreOfGrid(Grid<int> numbers, GridBool drawn)
    {
        int score = 0;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (!drawn.GetValue(x, y))
                {
                    score += numbers.GetValue(x, y);
                }
            }
        }

        return score;
    }
}
