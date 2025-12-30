using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe05b : IAufgabe
{
    public string Calc()
    {
        var input = Utilities.ReadInput(2024, 5);
        bool emptyLine = false;
        List<PageNumbers> settings = [];
        List<List<int>> wrongLists = [];
        int result = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line))
            {
                emptyLine = true;
                continue;
            }

            if (emptyLine)
            {
                List<int> numbers = line.Split(',').Select(x => int.Parse(x)).ToList();

                foreach (var setting in settings.Where(x => numbers.Contains(x.First) && numbers.Contains(x.Last)))
                {
                    var firstIndex = numbers.IndexOf(setting.First);
                    var lastIndex = numbers.IndexOf(setting.Last);

                    if (firstIndex > lastIndex)
                    {
                        wrongLists.Add(numbers);
                        break;
                    }
                }
            }
            else
            {
                var split = line.Split('|');
                settings.Add(new() { First = int.Parse(split[0]), Last = int.Parse(split[1]) });
            }
        }

        foreach (var wrongList in wrongLists)
        {
            List<int> repaired = [];

            foreach (var number in wrongList)
            {
                for (int i = 0; i <= repaired.Count; i++)
                {
                    List<int> repairedTest = [];
                    repairedTest.AddRange(repaired[..i]);
                    repairedTest.Add(number);
                    repairedTest.AddRange(repaired[i..]);
                    if (Check(settings, repairedTest))
                    {
                        repaired = repairedTest;
                        break;
                    }
                }
            }

            result += repaired[repaired.Count / 2];
        }

        return result.ToString();
    }

    private static bool Check(List<PageNumbers> settings, List<int> numbers)
    {
        foreach (var setting in settings.Where(x => numbers.Contains(x.First) && numbers.Contains(x.Last)))
        {
            var firstIndex = numbers.IndexOf(setting.First);
            var lastIndex = numbers.IndexOf(setting.Last);

            if (firstIndex > lastIndex)
            {
                return false;
            }
        }

        return true;
    }

    private class PageNumbers
    {
        public int First;
        public int Last;
    }
}
