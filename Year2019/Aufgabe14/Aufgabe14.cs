using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe14 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, (int Count, List<Chemical> Items)> _chemicals;

    public Aufgabe14()
    {
        _input = Utilities.ReadInput(2019, 14);
        _chemicals = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(' ');
            List<Chemical> inputChemicals = [];
            for (int i = 0; i < split.Length - 3; i += 2)
            {
                if (split[i + 1].EndsWith(','))
                {
                    split[i + 1] = split[i + 1][..^1];
                }
                inputChemicals.Add(new(int.Parse(split[i]), split[i + 1]));
            }

            _chemicals.Add(split[^1], (int.Parse(split[^2]), inputChemicals));
        }

        LargeCounter<string> neededChemicals = [];
        LargeCounter<string> currentChemicals = [];
        neededChemicals.Add("FUEL", 1);

        do
        {
            LargeCounter<string> nextNeededChemicals = [];

            foreach (var chemical in neededChemicals)
            {
                if (chemical.Key == "ORE")
                {
                    nextNeededChemicals.Add(chemical);
                    continue;
                }

                var neededCount = chemical.Value;
                var currentCount = currentChemicals.GetValueOrDefault(chemical.Key);
                if (currentCount > 0)
                {
                    if (currentCount >= neededCount)
                    {
                        currentChemicals.Decrease(chemical.Key, neededCount);
                        continue;
                    }
                    else
                    {
                        currentChemicals.Decrease(chemical.Key, currentCount);
                        neededCount -= currentCount;
                    }
                }

                var (recipeCount, recipeItems) = _chemicals[chemical.Key];
                var amount = (int)Math.Round((double)neededCount / recipeCount, MidpointRounding.ToPositiveInfinity);
                if (amount * recipeCount > (int)neededCount)
                {
                    currentChemicals.Add(chemical.Key, (ulong)(amount * recipeCount) - neededCount);
                }

                foreach (var newChemical in recipeItems)
                {
                    nextNeededChemicals.Add(newChemical.Name, (ulong)(newChemical.Value * amount));
                }
            }

            neededChemicals = nextNeededChemicals;
        } while (neededChemicals.Count > 1);

        return neededChemicals.First().Value.ToString();
    }

    private readonly record struct Chemical(int Value, string Name)
    {
    }
}
