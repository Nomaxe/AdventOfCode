using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe05 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe05()
    {
        _input = Utilities.ReadInput(2024, 5);
    }

    public string Calc()
    {
        bool emptyLine = false;
        List<PageNumbers> settings = [];
        int result = 0;
        foreach (var line in _input)
        {
            if (string.IsNullOrEmpty(line))
            {
                emptyLine = true;
                continue;
            }

            if (emptyLine)
            {
                List<int> numbers = line.Split(',').Select(x => int.Parse(x)).ToList();
                bool isOk = true;

                foreach (var setting in settings.Where(x => numbers.Contains(x.First) && numbers.Contains(x.Last)))
                {
                    var firstIndex = numbers.IndexOf(setting.First);
                    var lastIndex = numbers.IndexOf(setting.Last);

                    if (firstIndex > lastIndex)
                    {
                        isOk = false;
                        break;
                    }
                }

                if (isOk)
                {
                    result += numbers[numbers.Count / 2];
                }
            }
            else
            {
                var split = line.Split('|');
                settings.Add(new() { First = int.Parse(split[0]), Last = int.Parse(split[1]) });
            }
        }

        return result.ToString();
    }

    private class PageNumbers
    {
        public int First;
        public int Last;
    }
}
