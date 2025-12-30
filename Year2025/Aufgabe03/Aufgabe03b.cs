using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2025;

internal class Aufgabe03b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2025, 3);
    }

    public string Calc()
    {
        long result = 0;

        foreach (var line in _input)
        {
            var index = 0;
            StringBuilder builder = new(12);

            for (int i = 11; i >= 0; i--)
            {
                (var nextCharacter, var nextIndex) = GetNextCharacter(line[index..], i);
                index += nextIndex + 1;
                builder.Append(nextCharacter - '0');
            }

            result += long.Parse(builder.ToString());
        }

        return result.ToString();
    }

    private static (char Character, int Index) GetNextCharacter(string line, int numbersNeededAfter)
    {
        for (char i = '9'; i >= '0'; i--)
        {
            var index = line.IndexOf(i);

            if (index == -1 || index >= line.Length - numbersNeededAfter)
            {
                continue;
            }

            return (i, index);
        }

        throw new NotImplementedException();
    }
}
