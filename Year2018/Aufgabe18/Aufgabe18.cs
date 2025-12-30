using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe18 : IAufgabe
{
    private Grid _grid;

    public Aufgabe18()
    {
        _grid = Grid.CreateCharGrid(2018, 18);
    }

    public string Calc()
    {
        for (int i = 0; i < 10; i++)
        {
            Grid newGrid = new(_grid.SizeX, _grid.SizeY);

            for (int y = 0; y < _grid.SizeY; y++)
            {
                for (int x = 0; x < _grid.SizeX; x++)
                {
                    DictionaryCounter<char> counter = new(8);

                    foreach (var neighbour in _grid.GetInBoundFullNeighbours(x, y))
                    {
                        counter.Add(_grid.GetValue(neighbour));
                    }

                    switch (_grid.GetValue(x, y))
                    {
                        case '.':
                            newGrid.SetValue(x, y, counter.GetValueOrDefault('|') >= 3 ? '|' : '.');
                            break;
                        case '|':
                            newGrid.SetValue(x, y, counter.GetValueOrDefault('#') >= 3 ? '#' : '|');
                            break;
                        case '#':
                            newGrid.SetValue(x, y, counter.GetValueOrDefault('#') >= 1 && counter.GetValueOrDefault('|') >= 1 ? '#' : '.');
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }

            _grid = newGrid;
        }

        return (_grid.GetCountOfValue('|') * _grid.GetCountOfValue('#')).ToString();
    }
}
