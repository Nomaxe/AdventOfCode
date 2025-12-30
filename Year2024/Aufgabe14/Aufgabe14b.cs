using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe14b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<Robot> _robots = [];
    private const int SizeX = 101;
    private const int SizeY = 103;

    public Aufgabe14b()
    {
        _input = Utilities.ReadInput(2024, 14);
    }

    public string Calc()
    {
        int seconds = 0;

        foreach (var line in _input)
        {
            Regex regex = NumberRegex();
            var matches = regex.Matches(line);
            int positionX = Convert.ToInt32(matches[0].Value);
            int positionY = Convert.ToInt32(matches[1].Value);
            int velocityX = Convert.ToInt32(matches[2].Value);
            int velocityY = Convert.ToInt32(matches[3].Value);

            _robots.Add(new()
            {
                PositionX = positionX,
                PositionY = positionY,
                VelocityX = velocityX,
                VelocityY = velocityY
            });
        }

        while (true)
        {
            foreach (var robot in _robots)
            {
                robot.NextStep();
            }

            seconds++;

            if (_robots.GroupBy(x => new Point(x.PositionX, x.PositionY)).Max(x => x.Count()) == 1 && IsChrismasTree())
            {
                return seconds.ToString();
            }
        }
    }

    private bool IsChrismasTree()
    {
        Grid grid = new(SizeX, SizeY, ' ');

        foreach (var robot in _robots)
        {
            grid.SetValue(robot.PositionX, robot.PositionY, '#');
        }

        int count = 0;
        foreach (var character in grid)
        {
            if (character == '#')
            {
                count++;
                if (count >= 10)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }

        return false;
    }

    [GeneratedRegex(@"-?\d+")]
    private static partial Regex NumberRegex();

    private class Robot()
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public int VelocityX { get; init; }
        public int VelocityY { get; init; }

        public void NextStep()
        {
            PositionX += VelocityX;
            PositionY += VelocityY;

            if (PositionX < 0)
            {
                PositionX = SizeX + PositionX;
            }
            else if (PositionX >= SizeX)
            {
                PositionX -= SizeX;
            }

            if (PositionY < 0)
            {
                PositionY = SizeY + PositionY;
            }
            else if (PositionY >= SizeY)
            {
                PositionY -= SizeY;
            }
        }
    }
}
