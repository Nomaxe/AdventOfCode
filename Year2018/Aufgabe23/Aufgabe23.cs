using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe23 : IAufgabe
{
    private readonly Nanobot[] _nanobots;

    public Aufgabe23()
    {
        var input = Utilities.ReadInput(2018, 23);
        _nanobots = new Nanobot[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            _nanobots[i] = new(input[i]);
        }
    }

    public string Calc()
    {
        var checkNanobot = _nanobots.MaxBy(x => x.Radius);
        int count = 0;

        foreach (var nanobot in _nanobots)
        {
            if (checkNanobot.Point.GetManhattenDistance(nanobot.Point) <= checkNanobot.Radius)
            {
                count++;
            }
        }

        return count.ToString();
    }

    private readonly struct Nanobot
    {
        public Point3D Point { get; private init; }
        public int Radius { get; private init; }

        public Nanobot(string input)
        {
            var numbers = input.GetNumbers();
            Point = new(numbers[0], numbers[1], numbers[2]);
            Radius = numbers[3];
        }
    }
}
