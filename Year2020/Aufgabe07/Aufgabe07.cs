using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe07 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, string> _bags;

    public Aufgabe07()
    {
        _input = Utilities.ReadInput(2020, 7);
        _bags = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(" bag");
            if (split[1][10..] =="no other")
            {
                _bags.AddKey(split[0]);
                continue;
            }
            _bags.Add(GetFirstBag(split[1]), split[0]);

            for (int j = 2; j < split.Length - 1; j++)
            {
                _bags.Add(GetBag(split[j]), split[0]);
            }
        }

        Queue<string> queue = new();
        queue.Enqueue("shiny gold");
        HashSet<string> containingBags = new();

        while (queue.Count > 0)
        {
            var bag = queue.Dequeue();
            if (_bags.TryGetValue(bag, out var nextBags))
            {
                foreach (var nextBag in nextBags)
                {
                    if (!containingBags.Add(nextBag))
                    {
                        continue;
                    }

                    queue.Enqueue(nextBag);
                }
            }
        }

        return containingBags.Count.ToString();
    }

    private static string GetFirstBag(string input)
    {
        return input[12..];
    }

    private static string GetBag(string input)
    {
        if (input[0] == ',')
        {
            return input[4..];
        }
        else
        {
            return input[5..];
        }
    }
}
