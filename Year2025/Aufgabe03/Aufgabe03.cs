using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe03 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03()
    {
        _input = Utilities.ReadInput(2025, 3);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            (var firstCharacter, var firstIndex) = GetFirstCharacter(line);
            var secondCharacter = GetSecondCharacter(line[(firstIndex + 1)..]);

            result += (firstCharacter - '0') * 10 + (secondCharacter - '0');
        }

        return result.ToString();
    }

    private static (char Character, int Index) GetFirstCharacter(string line)
    {
        for (char i = '9'; i >= '0'; i--)
        {
            var index = line.IndexOf(i);

            if (index == -1 || index == line.Length - 1)
            {
                continue;
            }

            return (i, index);
        }

        throw new NotImplementedException();
    }

    private static char GetSecondCharacter(string line)
    {
        for (char i = '9'; i >= '0'; i--)
        {
            if (line.Contains(i))
            {
                return i;
            }
        }

        throw new NotImplementedException();
    }
}
