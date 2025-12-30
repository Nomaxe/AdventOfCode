using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe04b : IAufgabe
{
    private Grid _grid;
    private int _removedPaper = 0;

    public Aufgabe04b()
    {
        _grid = Grid.CreateCharGrid(2025, 4);
    }

    public string Calc()
    {
        //Optimierung: ES kann alles in einem Grid gemacht werden, da die sonst einfach im nächsten Durchlauf verschwinden würden

        int oldRemovedPaper;

        do
        {
            oldRemovedPaper = _removedPaper;
            _grid = GetNextGrid();
        } while (oldRemovedPaper != _removedPaper);

        return _removedPaper.ToString();
    }

    private Grid GetNextGrid()
    {
        Grid nextGrid = new(_grid.SizeX, _grid.SizeY);

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                if (_grid.GetValue(x, y) != '@')
                {
                    continue;
                }

                var count = 0;

                foreach (var neighbour in _grid.GetInBoundFullNeighbours(x, y))
                {
                    if (_grid.GetValue(neighbour) == '@')
                    {
                        count++;
                    }
                }

                if (count < 4)
                {
                    _removedPaper++;
                }
                else
                {
                    nextGrid.SetValue(x, y, '@');
                }
            }
        }

        return nextGrid;
    }
}
