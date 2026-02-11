using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe14b : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<string, (ulong Count, List<Chemical> Items)> _chemicals;

    public Aufgabe14b()
    {
        _input = Utilities.ReadInput(2019, 14);
        _chemicals = new(_input.Length);
    }

    public string Calc()
    {
        const ulong OreAmount = 1000000000000;

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
                inputChemicals.Add(new(ulong.Parse(split[i]), split[i + 1]));
            }

            _chemicals.Add(split[^1], (ulong.Parse(split[^2]), inputChemicals));
        }

        ulong min = 1;
        ulong max = OreAmount;

        while (max - min > 1)
        {
            var test = (min + max) / 2;
            var ore = GetOreAmount(test);
            if (ore > OreAmount)
            {
                max = test;
            }
            else
            {
                min = test;
            }
        }

        return min.ToString();
    }

    private ulong GetOreAmount(ulong fuelAmount)
    {
        LargeCounter<string> neededChemicals = [];
        LargeCounter<string> currentChemicals = [];
        neededChemicals.Add("FUEL", fuelAmount);

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
                var amount = (ulong)Math.Round((double)neededCount / recipeCount, MidpointRounding.ToPositiveInfinity);
                if (amount * recipeCount > neededCount)
                {
                    currentChemicals.Add(chemical.Key, (amount * recipeCount) - neededCount);
                }

                foreach (var newChemical in recipeItems)
                {
                    nextNeededChemicals.Add(newChemical.Name, (newChemical.Value * amount));
                }
            }

            neededChemicals = nextNeededChemicals;
        } while (neededChemicals.Count > 1);

        return neededChemicals.First().Value;
    }

    private readonly record struct Chemical(ulong Value, string Name)
    {
    }
}
