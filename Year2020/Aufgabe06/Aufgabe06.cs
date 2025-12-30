using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe06 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe06()
    {
        _input = Utilities.ReadInput(2020, 6);
    }

    public string Calc()
    {
        int count = 0;
        HashSet<char> correctAnswers = [];

        foreach (var line in _input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                count += correctAnswers.Count;
                correctAnswers.Clear();
                continue;
            }

            foreach (var character in line)
            {
                correctAnswers.Add(character);
            }
        }

        count += correctAnswers.Count;

        return count.ToString();
    }
}
