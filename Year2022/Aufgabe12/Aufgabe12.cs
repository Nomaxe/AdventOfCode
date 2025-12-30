using AdventOfCode.Utils;

namespace AdventOfCode.Year2022
{
    internal class Aufgabe12 : IAufgabe
    {
        private readonly Grid _grid;

        public Aufgabe12()
        {
            _grid = Grid.CreateCharGrid(2022, 12);
        }

        public string Calc()
        {
            HashSet<Point> visited = [];
            Point start = _grid.GetPointOfValue('S');
            Point end = _grid.GetPointOfValue('E');
            HashSet<Point> checkPoints = [start];
            _grid.SetValue(start, 'a');
            _grid.SetValue(end, 'z');
            int steps = 0;

            while (true)
            {
                HashSet<Point> nextCheckPoints = [];
                steps++;

                foreach (var checkPoint in checkPoints)
                {
                    var reachableHeight = (char)(_grid.GetValue(checkPoint) + 1);

                    foreach (var point in _grid.GetInBoundNeighbours(checkPoint).Where(x => !visited.Contains(x)))
                    {
                        if (_grid.GetValue(point) <= reachableHeight)
                        {
                            if (point == end)
                            {
                                return steps.ToString();
                            }

                            nextCheckPoints.Add(point);
                        }
                    }

                    visited.Add(checkPoint);
                }

                checkPoints = nextCheckPoints;
            }
        }
    }
}
