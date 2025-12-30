using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe24b : IAufgabe
{
    private List<GridBool> _grids;

    public Aufgabe24b()
    {
        _grids = [GridBool.CreateBoolGrid(2019, 24, '#')];
    }

    public string Calc()
    {
        for (int i = 0; i < 200; i++)
        {
            List<GridBool> newGrids = new(_grids.Count + 2);
            var outerGrid = CreateOuterGrid();
            if (outerGrid is not null)
            {
                newGrids.Add(outerGrid);
            }

            for (int j = 0; j < _grids.Count; j++)
            {
                newGrids.Add(CreateGrid(j));
            }

            var innerGrid = CreateInnerGrid();
            if (innerGrid is not null)
            {
                newGrids.Add(innerGrid);
            }

            _grids = newGrids;
        }

        var count = 0;
        foreach (var grid in _grids)
        {
            count += grid.GetCountOfValue(true);
        }
        return count.ToString();
    }

    private int GetBugCount00(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(1, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 1))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 1))
            {
                count++;
            }
            if (_grids[dimension - 1].GetValue(1, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount10(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(2, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 0))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 1))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount20(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(3, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 0))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 1))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount30(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(4, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 0))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 1))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount40(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(4, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 0))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 1))
            {
                count++;
            }
            if (_grids[dimension - 1].GetValue(3, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount01(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(0, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 2))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(1, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount11(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(1, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 1))
        {
            count++;
        }

        return count;
    }

    private int GetBugCount21(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(2, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 1))
        {
            count++;
        }
        count += GetBugCountHorizontal(dimension + 1, 0);

        return count;
    }

    private int GetBugCount31(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(3, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(4, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 1))
        {
            count++;
        }

        return count;
    }

    private int GetBugCount41(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(4, 0))
        {
            count++;
        }
        if (_grids[dimension].GetValue(4, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 1))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(3, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount02(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(0, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 3))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(1, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount12(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(1, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 2))
        {
            count++;
        }
        count += GetBugCountVertical(dimension + 1, 0);

        return count;
    }

    private int GetBugCount32(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(3, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(4, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 3))
        {
            count++;
        }
        count += GetBugCountVertical(dimension + 1, 4);

        return count;
    }

    private int GetBugCount42(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(4, 1))
        {
            count++;
        }
        if (_grids[dimension].GetValue(4, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 2))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(3, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount03(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(0, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 4))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(1, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount13(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(1, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 4))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 3))
        {
            count++;
        }

        return count;
    }

    private int GetBugCount23(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(3, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 4))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 3))
        {
            count++;
        }
        count += GetBugCountHorizontal(dimension + 1, 4);

        return count;
    }

    private int GetBugCount33(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(3, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(4, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 4))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 3))
        {
            count++;
        }

        return count;
    }

    private int GetBugCount43(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(4, 2))
        {
            count++;
        }
        if (_grids[dimension].GetValue(4, 4))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 3))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(3, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount04(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(0, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 4))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 3))
            {
                count++;
            }
            if (_grids[dimension - 1].GetValue(1, 2))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount14(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(1, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 4))
        {
            count++;
        }
        if (_grids[dimension].GetValue(0, 4))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 3))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount24(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(2, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 4))
        {
            count++;
        }
        if (_grids[dimension].GetValue(1, 4))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 3))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount34(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(3, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(4, 4))
        {
            count++;
        }
        if (_grids[dimension].GetValue(2, 4))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(2, 3))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCount44(int dimension)
    {
        int count = 0;

        if (_grids[dimension].GetValue(4, 3))
        {
            count++;
        }
        if (_grids[dimension].GetValue(3, 4))
        {
            count++;
        }
        if (dimension > 0)
        {
            if (_grids[dimension - 1].GetValue(3, 2))
            {
                count++;
            }
            if (_grids[dimension - 1].GetValue(2, 3))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCountHorizontal(int dimension, int y)
    {
        if (dimension < 0 || dimension >= _grids.Count)
        {
            return 0;
        }

        int count = 0;

        for (int x = 0; x < 5; x++)
        {
            if (_grids[dimension].GetValue(x, y))
            {
                count++;
            }
        }

        return count;
    }

    private int GetBugCountVertical(int dimension, int x)
    {
        if (dimension < 0 || dimension >= _grids.Count)
        {
            return 0;
        }

        int count = 0;

        for (int y = 0; y < 5; y++)
        {
            if (_grids[dimension].GetValue(x, y))
            {
                count++;
            }
        }

        return count;
    }

    private GridBool? CreateOuterGrid()
    {
        GridBool? grid = null;

        if (GetNewState(false, GetBugCountHorizontal(0, 0)))
        {
            grid ??= new(5);
            grid.SetValue(2, 1, true);
        }
        if (GetNewState(false, GetBugCountHorizontal(0, 4)))
        {
            grid ??= new(5);
            grid.SetValue(2, 3, true);
        }
        if (GetNewState(false, GetBugCountVertical(0, 0)))
        {
            grid ??= new(5);
            grid.SetValue(1, 2, true);
        }
        if (GetNewState(false, GetBugCountVertical(0, 4)))
        {
            grid ??= new(5);
            grid.SetValue(3, 2, true);
        }

        return grid;
    }

    private GridBool CreateGrid(int dimension)
    {
        var grid = _grids[dimension];
        GridBool newGrid = new(5);

        newGrid.SetValue(0, 0, GetNewState(grid.GetValue(0, 0), GetBugCount00(dimension)));
        newGrid.SetValue(0, 1, GetNewState(grid.GetValue(0, 1), GetBugCount01(dimension)));
        newGrid.SetValue(0, 2, GetNewState(grid.GetValue(0, 2), GetBugCount02(dimension)));
        newGrid.SetValue(0, 3, GetNewState(grid.GetValue(0, 3), GetBugCount03(dimension)));
        newGrid.SetValue(0, 4, GetNewState(grid.GetValue(0, 4), GetBugCount04(dimension)));

        newGrid.SetValue(1, 0, GetNewState(grid.GetValue(1, 0), GetBugCount10(dimension)));
        newGrid.SetValue(1, 1, GetNewState(grid.GetValue(1, 1), GetBugCount11(dimension)));
        newGrid.SetValue(1, 2, GetNewState(grid.GetValue(1, 2), GetBugCount12(dimension)));
        newGrid.SetValue(1, 3, GetNewState(grid.GetValue(1, 3), GetBugCount13(dimension)));
        newGrid.SetValue(1, 4, GetNewState(grid.GetValue(1, 4), GetBugCount14(dimension)));

        newGrid.SetValue(2, 0, GetNewState(grid.GetValue(2, 0), GetBugCount20(dimension)));
        newGrid.SetValue(2, 1, GetNewState(grid.GetValue(2, 1), GetBugCount21(dimension)));
        newGrid.SetValue(2, 3, GetNewState(grid.GetValue(2, 3), GetBugCount23(dimension)));
        newGrid.SetValue(2, 4, GetNewState(grid.GetValue(2, 4), GetBugCount24(dimension)));

        newGrid.SetValue(3, 0, GetNewState(grid.GetValue(3, 0), GetBugCount30(dimension)));
        newGrid.SetValue(3, 1, GetNewState(grid.GetValue(3, 1), GetBugCount31(dimension)));
        newGrid.SetValue(3, 2, GetNewState(grid.GetValue(3, 2), GetBugCount32(dimension)));
        newGrid.SetValue(3, 3, GetNewState(grid.GetValue(3, 3), GetBugCount33(dimension)));
        newGrid.SetValue(3, 4, GetNewState(grid.GetValue(3, 4), GetBugCount34(dimension)));

        newGrid.SetValue(4, 0, GetNewState(grid.GetValue(4, 0), GetBugCount40(dimension)));
        newGrid.SetValue(4, 1, GetNewState(grid.GetValue(4, 1), GetBugCount41(dimension)));
        newGrid.SetValue(4, 2, GetNewState(grid.GetValue(4, 2), GetBugCount42(dimension)));
        newGrid.SetValue(4, 3, GetNewState(grid.GetValue(4, 3), GetBugCount43(dimension)));
        newGrid.SetValue(4, 4, GetNewState(grid.GetValue(4, 4), GetBugCount44(dimension)));

        return newGrid;
    }

    private GridBool? CreateInnerGrid()
    {
        GridBool? grid = null;
        int index = _grids.Count - 1;

        if (_grids[index].GetValue(2, 1))
        {
            grid ??= new(5);
            grid.SetValue(0, 0, true);
            grid.SetValue(1, 0, true);
            grid.SetValue(2, 0, true);
            grid.SetValue(3, 0, true);
            grid.SetValue(4, 0, true);
        }
        if (_grids[index].GetValue(3, 2))
        {
            grid ??= new(5);
            grid.SetValue(4, 0, true);
            grid.SetValue(4, 1, true);
            grid.SetValue(4, 2, true);
            grid.SetValue(4, 3, true);
            grid.SetValue(4, 4, true);
        }
        if (_grids[index].GetValue(2, 3))
        {
            grid ??= new(5);
            grid.SetValue(0, 4, true);
            grid.SetValue(1, 4, true);
            grid.SetValue(2, 4, true);
            grid.SetValue(3, 4, true);
            grid.SetValue(4, 4, true);
        }
        if (_grids[index].GetValue(1, 2))
        {
            grid ??= new(5);
            grid.SetValue(0, 0, true);
            grid.SetValue(0, 1, true);
            grid.SetValue(0, 2, true);
            grid.SetValue(0, 3, true);
            grid.SetValue(0, 4, true);
        }

        return grid;
    }

    private static bool GetNewState(bool oldState, int bugCount)
    {
        if (oldState)
        {
            return bugCount == 1;
        }
        else
        {
            return bugCount == 1 || bugCount == 2;
        }
    }
}
