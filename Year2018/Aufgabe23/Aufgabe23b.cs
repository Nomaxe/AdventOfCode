using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe23b : IAufgabe
{
    private readonly Nanobot[] _nanobots;

    public Aufgabe23b()
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
        PriorityQueue<Box, long> queue = new();
        queue.Enqueue(new(new(0, 0, 0), 0x40_00_00_00), 0);

        while (true)
        {
            var box = queue.Dequeue();

            if (box.Size == 1)
            {
                return box.Center.GetManhattenDistanceToZero().ToString();
            }

            var bestBoxes = GetBestBoxes(box, out int bestCount);
            foreach (var bestBox in bestBoxes)
            {
                long priority = _nanobots.Length - bestCount;
                priority <<= 32;
                priority += bestBox.Center.GetManhattenDistanceToZero();
                queue.Enqueue(bestBox, priority);
            }
        }
    }

    private List<Box> GetBestBoxes(Box box, out int bestCount)
    {
        int newBoxSize = box.Size / 2;
        int newBoxRadius = box.Radius / 2;

        if (newBoxRadius == 0)
        {
            newBoxRadius++;
        }

        List<Box> bestBoxes = new();

        Point3D checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y - newBoxRadius, box.Center.Z - newBoxRadius);
        int checkCount = GetCountOfBox(checkCenter, newBoxSize);
        bestBoxes.Add(new(checkCenter, newBoxSize));
        bestCount = checkCount;

        checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y - newBoxRadius, box.Center.Z + newBoxRadius);
        checkCount = GetCountOfBox(checkCenter, newBoxSize);
        if (checkCount > bestCount)
        {
            bestBoxes.Clear();
            bestBoxes.Add(new(checkCenter, newBoxSize));
            bestCount = checkCount;
        }
        else if (checkCount == bestCount)
        {
            bestBoxes.Add(new(checkCenter, newBoxSize));
        }

        checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y + newBoxRadius, box.Center.Z - newBoxRadius);
        checkCount = GetCountOfBox(checkCenter, newBoxSize);
        if (checkCount > bestCount)
        {
            bestBoxes.Clear();
            bestBoxes.Add(new(checkCenter, newBoxSize));
            bestCount = checkCount;
        }
        else if (checkCount == bestCount)
        {
            bestBoxes.Add(new(checkCenter, newBoxSize));
        }

        checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y + newBoxRadius, box.Center.Z + newBoxRadius);
        checkCount = GetCountOfBox(checkCenter, newBoxSize);
        if (checkCount > bestCount)
        {
            bestBoxes.Clear();
            bestBoxes.Add(new(checkCenter, newBoxSize));
            bestCount = checkCount;
        }
        else if (checkCount == bestCount)
        {
            bestBoxes.Add(new(checkCenter, newBoxSize));
        }

        checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y - newBoxRadius, box.Center.Z - newBoxRadius);
        checkCount = GetCountOfBox(checkCenter, newBoxSize);
        if (checkCount > bestCount)
        {
            bestBoxes.Clear();
            bestBoxes.Add(new(checkCenter, newBoxSize));
            bestCount = checkCount;
        }
        else if (checkCount == bestCount)
        {
            bestBoxes.Add(new(checkCenter, newBoxSize));
        }

        checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y - newBoxRadius, box.Center.Z + newBoxRadius);
        checkCount = GetCountOfBox(checkCenter, newBoxSize);
        if (checkCount > bestCount)
        {
            bestBoxes.Clear();
            bestBoxes.Add(new(checkCenter, newBoxSize));
            bestCount = checkCount;
        }
        else if (checkCount == bestCount)
        {
            bestBoxes.Add(new(checkCenter, newBoxSize));
        }

        checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y + newBoxRadius, box.Center.Z - newBoxRadius);
        checkCount = GetCountOfBox(checkCenter, newBoxSize);
        if (checkCount > bestCount)
        {
            bestBoxes.Clear();
            bestBoxes.Add(new(checkCenter, newBoxSize));
            bestCount = checkCount;
        }
        else if (checkCount == bestCount)
        {
            bestBoxes.Add(new(checkCenter, newBoxSize));
        }

        checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y + newBoxRadius, box.Center.Z + newBoxRadius);
        checkCount = GetCountOfBox(checkCenter, newBoxSize);
        if (checkCount > bestCount)
        {
            bestBoxes.Clear();
            bestBoxes.Add(new(checkCenter, newBoxSize));
            bestCount = checkCount;
        }
        else if (checkCount == bestCount)
        {
            bestBoxes.Add(new(checkCenter, newBoxSize));
        }

        if (newBoxSize == 1)
        {
            checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y + newBoxRadius, box.Center.Z);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y - newBoxRadius, box.Center.Z);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y + newBoxRadius, box.Center.Z);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y - newBoxRadius, box.Center.Z);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y, box.Center.Z - newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X - newBoxRadius, box.Center.Y, box.Center.Z + newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y, box.Center.Z - newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X + newBoxRadius, box.Center.Y, box.Center.Z + newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X, box.Center.Y - newBoxRadius, box.Center.Z - newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X, box.Center.Y - newBoxRadius, box.Center.Z + newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X, box.Center.Y + newBoxRadius, box.Center.Z - newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }

            checkCenter = new(box.Center.X, box.Center.Y + newBoxRadius, box.Center.Z + newBoxRadius);
            checkCount = GetCountOfBox(checkCenter, newBoxSize);
            if (checkCount > bestCount)
            {
                bestBoxes.Clear();
                bestBoxes.Add(new(checkCenter, newBoxSize));
                bestCount = checkCount;
            }
            else if (checkCount == bestCount)
            {
                bestBoxes.Add(new(checkCenter, newBoxSize));
            }
        }

        return bestBoxes;
    }

    private int GetCountOfBox(Point3D center, int boxSize)
    {
        int boxRadius = boxSize / 2;
        Point3D boxMin = new(center.X - boxRadius, center.Y - boxRadius, center.Z - boxRadius);
        Point3D boxMax = new(center.X + boxRadius, center.Y + boxRadius, center.Z + boxRadius);

        return _nanobots.Count(x => x.IsOverlapping(boxMin, boxMax));
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

        public bool IsOverlapping(Point3D boxMin, Point3D boxMax)
        {
            var distance = 0;

            distance += GetDistance(Point.X, boxMin.X, boxMax.X);
            distance += GetDistance(Point.Y, boxMin.Y, boxMax.Y);
            distance += GetDistance(Point.Z, boxMin.Z, boxMax.Z);

            return Radius >= distance;
        }

        private static int GetDistance(int value, int min, int max)
        {
            if (value >= min && value <= max)
            {
                return 0;
            }

            if (value < min)
            {
                return min - value;
            }
            else
            {
                return value - max;
            }
        }
    }

    private readonly struct Box
    {
        public Point3D Center { get; private init; }
        public int Size { get; private init; }

        public int Radius => Size / 2;

        public Box(Point3D center, int size)
        {
            Center = center;
            Size = size;
        }

        public override string ToString()
        {
            return $"{Center} ({Size})";
        }
    }
}
