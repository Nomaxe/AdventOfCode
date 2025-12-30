using AdventOfCode.Utils;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2016;

internal class Aufgabe17 : IAufgabe
{
    private readonly string _input;

    public Aufgabe17()
    {
        _input = Utilities.ReadInputAsString(2016, 17);
    }

    public string Calc()
    {
        List<Possibility> possibilities = [new(0, 0, _input)];

        while (possibilities.Count > 0)
        {
            List<Possibility> nextPossibilities = new(possibilities.Count);

            foreach (var possibility in possibilities)
            {
                var hash = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes(possibility.Path)));

                if (possibility.Y > 0 && IsDoorOpen(hash[0]))
                {
                    nextPossibilities.Add(new(possibility.X, possibility.Y - 1, $"{possibility.Path}U"));
                }

                if (possibility.Y < 3 && IsDoorOpen(hash[1]))
                {
                    var nextPath = $"{possibility.Path}D";
                    if (possibility.X == 3 && possibility.Y == 2)
                    {
                        return nextPath[_input.Length..];
                    }
                    nextPossibilities.Add(new(possibility.X, possibility.Y + 1, $"{possibility.Path}D"));
                }

                if (possibility.X > 0 && IsDoorOpen(hash[2]))
                {
                    nextPossibilities.Add(new(possibility.X - 1, possibility.Y, $"{possibility.Path}L"));
                }

                if (possibility.X < 3 && IsDoorOpen(hash[3]))
                {
                    var nextPath = $"{possibility.Path}R";
                    if (possibility.X == 2 && possibility.Y == 3)
                    {
                        return nextPath[_input.Length..];
                    }
                    nextPossibilities.Add(new(possibility.X + 1, possibility.Y, $"{possibility.Path}R"));
                }
            }

            possibilities = nextPossibilities;
        }

        throw new NotImplementedException();
    }

    private static bool IsDoorOpen(char character)
    {
        return character >= 'B';
    }

    private readonly struct Possibility(int x, int y, string path)
    {
        public int X { get; private init; } = x;
        public int Y { get; private init; } = y;
        public string Path { get; private init; } = path;
    }
}
