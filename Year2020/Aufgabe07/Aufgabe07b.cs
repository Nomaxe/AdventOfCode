using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe07b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, Bag> _bags;

    public Aufgabe07b()
    {
        _input = Utilities.ReadInput(2020, 7);
        _bags = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(" bag");
            if (split[1][10..] == "no other")
            {
                _bags.AddKey(split[0]);
                continue;
            }

            List<Bag> bags = new(split.Length - 1)
            {
                GetFirstBag(split[1])
            };

            for (int j = 2; j < split.Length - 1; j++)
            {
                bags.Add(GetBag(split[j]));
            }

            _bags.Add(split[0], bags);
        }

        Queue<Bag> queue = new();
        queue.Enqueue(new(1, "shiny gold"));
        int result = -1; //Die shiny gold zählt nicht mit

        while (queue.Count > 0)
        {
            var bag = queue.Dequeue();
            var nextBags = _bags[bag.Type];
            result += bag.Count;

            foreach (var nextBag in nextBags)
            {
                queue.Enqueue(new(bag.Count * nextBag.Count, nextBag.Type));
            }
        }

        return result.ToString();
    }

    private static Bag GetFirstBag(string input)
    {
        return new(input[10..]);
    }

    private static Bag GetBag(string input)
    {
        if (input[0] == ',')
        {
            return new(input[2..]);
        }
        else
        {
            return new(input[3..]);
        }
    }

    private readonly struct Bag
    {
        public readonly int Count { get; private init; }
        public readonly string Type { get; private init; }

        public Bag(int count, string type)
        {
            Count = count;
            Type = type;
        }

        public Bag(string input)
        {
            Count = input[0].ToNumber();
            Type = input[2..];
        }

        public override string ToString()
        {
            return $"{Count} {Type}";
        }
    }
}
