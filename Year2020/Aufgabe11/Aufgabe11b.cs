using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe11b : IAufgabe
{
    private Grid _grid;

    public Aufgabe11b()
    {
        _grid = Grid.CreateCharGrid(2020, 11);
    }

    public string Calc()
    {
        bool equals;

        do
        {
            Grid nextGrid = new(_grid.SizeX, _grid.SizeY, '.');
            for (int y = 0; y < _grid.SizeY; y++)
            {
                for (int x = 0; x < _grid.SizeX; x++)
                {
                    switch (_grid.GetValue(x, y))
                    {
                        case 'L':
                            nextGrid.SetValue(x, y, GetNeighbourCount(x, y) == 0 ? '#' : 'L');
                            break;
                        case '#':
                            nextGrid.SetValue(x, y, GetNeighbourCount(x, y) >= 5 ? 'L' : '#');
                            break;
                        case '.':
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            equals = _grid == nextGrid;
            _grid = nextGrid;
        } while (!equals);

        return _grid.GetCountOfValue('#').ToString();
    }

    private int GetNeighbourCount(int x, int y)
    {
        return GetNeighbourInDirection(x, y, 1, 1) +
               GetNeighbourInDirection(x, y, 1, 0) +
               GetNeighbourInDirection(x, y, 1, -1) +
               GetNeighbourInDirection(x, y, 0, 1) +
               GetNeighbourInDirection(x, y, 0, -1) +
               GetNeighbourInDirection(x, y, -1, 1) +
               GetNeighbourInDirection(x, y, -1, 0) +
               GetNeighbourInDirection(x, y, -1, -1);
    }

    private int GetNeighbourInDirection(int x, int y, int xDirection, int yDirection)
    {
        x += xDirection;
        y += yDirection;

        while (_grid.IsInBounds(x, y))
        {
            switch (_grid.GetValue(x, y))
            {
                case '#':
                    return 1;
                case 'L':
                    return 0;
            }

            x += xDirection;
            y += yDirection;
        }

        return 0;
    }
}
