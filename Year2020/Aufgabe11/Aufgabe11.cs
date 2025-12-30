using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe11 : IAufgabe
{
    private Grid<char> _grid;

    public Aufgabe11()
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
                            nextGrid.SetValue(x, y, GetNeighbourCount(x, y) >= 4 ? 'L' : '#');
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
        return new Point(x, y).GetFullNeighbours().Where(_grid.IsInBounds).Where(x => _grid.GetValue(x) == '#').Count();
    }
}
