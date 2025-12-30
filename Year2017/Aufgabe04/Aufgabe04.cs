using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe04 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04()
    {
        _input = Utilities.ReadInput(2017, 4);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            HashSet<string> phrases = [];
            bool unique = true;

            var split = line.Split(' ');
            foreach (var item in split)
            {
                if (phrases.Contains(item))
                {
                    unique = false;
                    break;
                }

                phrases.Add(item);
            }

            if (unique)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
