using AdventOfCode.Utils;

namespace AdventOfCode.Year2022
{
    internal class Aufgabe12b : IAufgabe
    {
        private readonly Grid _grid;

        public Aufgabe12b()
        {
            _grid = Grid.CreateCharGrid(2022, 12);
        }

        public string Calc()
        {
            HashSet<Point> visited = [];
            Point start = _grid.GetPointOfValue('E');
            HashSet<Point> checkPoints = [start];
            _grid.SetValue(_grid.GetPointOfValue('S'), 'a');
            _grid.SetValue(start, 'z');
            int steps = 0;

            while (true)
            {
                HashSet<Point> nextCheckPoints = [];
                steps++;

                foreach (var checkPoint in checkPoints)
                {
                    var reachableHeight = (char)(_grid.GetValue(checkPoint) - 1);

                    foreach (var point in _grid.GetInBoundNeighbours(checkPoint).Where(x => !visited.Contains(x)))
                    {
                        var height = _grid.GetValue(point);

                        if (height >= reachableHeight)
                        {
                            if (height == 'a')
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
