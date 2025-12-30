using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe15 : IAufgabe
{
    private readonly List<(int X, int Y, int ClosestX, int ClosestY)> _beacons;

    public Aufgabe15()
    {
        var input = Utilities.ReadInput(2022, 15);
        _beacons = new(input.Length);

        foreach (var line in input)
        {
            var split = line.GetNumbers();
            _beacons.Add((split[0], split[1], split[2], split[3]));
        }
    }

    public string Calc()
    {
        const int CheckY = 2000000;
        HashSet<int> notPossiblePositions = [];

        foreach (var beacon in _beacons)
        {
            int distance = int.Max(beacon.X, beacon.ClosestX) - int.Min(beacon.X, beacon.ClosestX) +
                           int.Max(beacon.Y, beacon.ClosestY) - int.Min(beacon.Y, beacon.ClosestY);
            int distanceToY = int.Abs(beacon.Y - CheckY);

            if (distance >= distanceToY)
            {
                notPossiblePositions.Add(beacon.X);

                for (int i = 1; i <= distance - distanceToY; i++)
                {
                    notPossiblePositions.Add(beacon.X + i);
                    notPossiblePositions.Add(beacon.X - i);
                }
            }
        }

        foreach (var beacon in _beacons.Where(x => x.ClosestY == CheckY))
        {
            notPossiblePositions.Remove(beacon.ClosestX);
        }

        return notPossiblePositions.Count.ToString();
    }
}
