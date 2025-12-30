using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe25 : IAufgabe
{
    private readonly List<Point4D> _points;
    private readonly List<List<Point4D>> _constellations;

    public Aufgabe25()
    {
        _points = Utilities.ReadInput(2018, 25)
                           .Select(x => new Point4D(x))
                           .ToList();
        _constellations = new();
    }

    public string Calc()
    {
        foreach (var point in _points)
        {
            AddPoint(point);
        }

        Collect();

        return _constellations.Count.ToString();

        //<504
    }

    private void AddPoint(Point4D point)
    {
        foreach (var constellation in _constellations)
        {
            foreach (var pointInConstellation in constellation)
            {
                if (point.GetManhattenDistance(pointInConstellation) <= 3)
                {
                    constellation.Add(point);
                    return;
                }
            }
        }

        _constellations.Add([point]);
    }

    private void Collect()
    {
        for (int i = 0; i < _constellations.Count; i++)
        {
            for (int j = i + 1; j < _constellations.Count; j++)
            {
                if (IsOverlapping(_constellations[i], _constellations[j]))
                {
                    _constellations[i].AddRange(_constellations[j]);
                    _constellations.RemoveAt(j);
                    i--;
                    break;
                }
            }
        }
    }

    private static bool IsOverlapping(List<Point4D> list1, List<Point4D> list2)
    {
        foreach (var point1 in list1)
        {
            foreach (var point2 in list2)
            {
                if (point1.GetManhattenDistance(point2) <= 3)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
