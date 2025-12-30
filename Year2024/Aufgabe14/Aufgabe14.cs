using AdventOfCode.Utils;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2024;

internal partial class Aufgabe14 : IAufgabe
{
    private readonly string[] _input;
    private const int SizeX = 101;
    private const int SizeY = 103;

    public Aufgabe14()
    {
        _input = Utilities.ReadInput(2024, 14);
    }

    public string Calc()
    {
        int topLeft = 0;
        int topRight = 0;
        int bottomLeft = 0;
        int bottomRight = 0;
        int middle = 0;

        foreach (var line in _input)
        {
            Regex regex = NumberRegex();
            var matches = regex.Matches(line);
            int positionX = Convert.ToInt32(matches[0].Value);
            int positionY = Convert.ToInt32(matches[1].Value);
            int velocityX = Convert.ToInt32(matches[2].Value);
            int velocityY = Convert.ToInt32(matches[3].Value);

            for (int i = 1; i <= 100; i++)
            {
                positionX += velocityX;
                positionY += velocityY;

                if (positionX < 0)
                {
                    positionX = SizeX + positionX;
                }
                else if (positionX >= SizeX)
                {
                    positionX -= SizeX;
                }

                if (positionY < 0)
                {
                    positionY = SizeY + positionY;
                }
                else if (positionY >= SizeY)
                {
                    positionY -= SizeY;
                }
            }

            if (positionX < SizeX / 2)
            {
                if (positionY < SizeY / 2)
                {
                    topLeft++;
                    continue;
                }
                else if (positionY > SizeY / 2)
                {
                    bottomLeft++;
                    continue;
                }
            }
            else if (positionX > SizeX / 2)
            {
                if (positionY < SizeY / 2)
                {
                    topRight++;
                    continue;
                }
                else if (positionY > SizeY / 2)
                {
                    bottomRight++;
                    continue;
                }
            }

            middle++;
        }

        return (topLeft * topRight * bottomLeft * bottomRight).ToString();
    }

    [GeneratedRegex(@"-?\d+")]
    private static partial Regex NumberRegex();
}
