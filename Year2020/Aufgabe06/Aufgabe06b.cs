using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe06b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe06b()
    {
        _input = Utilities.ReadInput(2020, 6);
    }

    public string Calc()
    {
        int count = 0;
        int people = 0;
        LargeCounter<char> correctAnswers = [];

        foreach (var line in _input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                count += GetCorrectAnswers(people, correctAnswers);
                correctAnswers.Clear();
                people = 0;
                continue;
            }

            foreach (var character in line)
            {
                correctAnswers.Add(character);
            }
            people++;
        }

        count += GetCorrectAnswers(people, correctAnswers);

        return count.ToString();
    }

    private static int GetCorrectAnswers(int people, LargeCounter<char> correctAnswers)
    {
        ulong peopleUlong = (ulong)people;
        return correctAnswers.Count(x => x.Value == peopleUlong);
    }
}
