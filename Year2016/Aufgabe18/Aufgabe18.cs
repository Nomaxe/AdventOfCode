using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe18 : IAufgabe
{
    private readonly char[] _input;

    public Aufgabe18()
    {
        _input = Utilities.ReadInputAsString(2016, 18).ToCharArray();
    }

    public string Calc()
    {
        int count = _input.Count(x => x == '.');
        char[] row = _input;

        for (int i = 0; i < 39; i++)
        {
            char[] nextRow = new char[row.Length];

            nextRow[0] = GetCharacter('.', row[1]);
            nextRow[^1] = GetCharacter(row[^2], '.');

            for (int j = 1; j < row.Length - 1; j++)
            {
                nextRow[j] = GetCharacter(row[j - 1], row[j + 1]);
            }

            row = nextRow;
            count += row.Count(x => x == '.');
        }

        return count.ToString();
    }

    private static char GetCharacter(char left, char right)
    {
        if (left == '^' && right == '.')
        {
            return '^';
        }

        if (left == '.' && right == '^')
        {
            return '^';
        }

        return '.';
    }
}
