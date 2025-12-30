using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe04b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04b()
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
                string ordered = new([.. item.Order()]);

                if (phrases.Contains(ordered))
                {
                    unique = false;
                    break;
                }

                phrases.Add(ordered);
            }

            if (unique)
            {
                result++;
            }
        }

        return result.ToString();
    }
}
