using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe04 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04()
    {
        _input = Utilities.ReadInput(2016, 4);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            var checksum = line[^6..^1];
            var split = line[..^7].Split('-');
            LargeCounter<char> counter = [];
            foreach (var name in split.Take(split.Length - 1))
            {
                foreach (var character in name)
                {
                    counter.Add(character);
                }
            }
            string nameChecksum = new(counter.OrderByDescending(x => x.Value).ThenBy(x => x.Key).Select(x => x.Key).Take(5).ToArray());

            if (nameChecksum == checksum)
            {
                result += int.Parse(split[^1]);
            }
        }

        return result.ToString();
    }
}
