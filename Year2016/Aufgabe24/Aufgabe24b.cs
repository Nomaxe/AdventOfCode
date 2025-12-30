using AdventOfCode.Utils;
using AdventOfCode.Utils.Labyrinth;

namespace AdventOfCode.Year2016;

internal class Aufgabe24b : IAufgabe
{
    private readonly Grid _grid;
    private readonly DictionaryDictionary<int, int, int> _lengths;
    private readonly Dictionary<int, Point> _points;

    public Aufgabe24b()
    {
        _grid = Grid.CreateCharGrid(2016, 24);
        _lengths = new();
        _points = new();
        for (int i = 0; i < int.MaxValue; i++)
        {
            var point = _grid.GetPointOfValueOrNull((char)(i + '0'));
            if (!point.HasValue)
            {
                break;
            }

            _points.Add(i, point.Value);
        }
    }

    public string Calc()
    {
        for (int i = 0; i < _points.Count; i++)
        {
            CompleteSolver solver = new(_grid);
            solver.SolveLabyrinth(_points[i]);

            for (int j = 0; j < _points.Count; j++)
            {
                if (i == j)
                {
                    continue;
                }

                _lengths.Add(i, j, solver.GetLength(_points[j]));
            }
        }

        PermuationGenerator<int> generator = new(_points.Keys.Where(x => x != 0).ToList());
        int minLength = int.MaxValue;

        foreach (var permutation in generator.GetPermuations())
        {
            int length = _lengths.GetValue(0, permutation[0]);
            for (int i = 1; i < permutation.Count; i++)
            {
                length += _lengths.GetValue(permutation[i - 1], permutation[i]);
            }
            length += _lengths.GetValue(permutation[^1], 0);

            minLength = int.Min(minLength, length);
        }

        return minLength.ToString();
    }
}
