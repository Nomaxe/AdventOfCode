using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe24 : IAufgabe
{
    private Grid _grid;
    private readonly HashSet<int> _rating;

    public Aufgabe24()
    {
        _grid = Grid.CreateCharGrid(2019, 24);
        _rating = new();
    }

    public string Calc()
    {
        _rating.Add(GetBiodiversityRating());

        while (true)
        {
            Grid nextGrid = new(5);

            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    var count = GetBugCount(x, y);

                    if (_grid.GetValue(x, y) == '#')
                    {
                        if (count == 1)
                        {
                            nextGrid.SetValue(x, y, '#');
                        }
                    }
                    else
                    {
                        if (count == 1 || count == 2)
                        {
                            nextGrid.SetValue(x, y, '#');
                        }
                    }
                }
            }

            _grid = nextGrid;
            var rating = GetBiodiversityRating();

            if (!_rating.Add(rating))
            {
                return rating.ToString();
            }
        }
    }

    private int GetBugCount(int x, int y)
    {
        int count = 0;

        foreach (var neighbour in _grid.GetInBoundNeighbours(x, y))
        {
            if (_grid.GetValue(neighbour) == '#')
            {
                count++;
            }
        }

        return count;
    }

    private int GetBiodiversityRating()
    {
        int rating = 0;

        for (int y = 0; y < 5; y++)
        {
            for (int x = 0; x < 5; x++)
            {
                if (_grid.GetValue(x, y) == '#')
                {
                    rating += Convert.ToInt32(Math.Pow(2, y * 5 + x));
                }
            }
        }

        return rating;
    }
}
