using AdventOfCode.Utils;

namespace AdventOfCode.Year2023;

internal class Aufgabe09 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe09()
    {
        _input = Utilities.ReadInput(2023, 9);
    }

    public string Calc()
    {
        long result = 0;

        foreach (var line in _input)
        {
            var split = line.Split(' ');
            List<long> list = new(split.Length);
            list.AddRange(split.Select(long.Parse));
            result += Calc(list) + list[^1];
        }

        return result.ToString();
    }

    private static long Calc(List<long> input)
    {
        List<long> list = new(input.Count - 1);

        for (int i = 0; i < input.Count - 1; i++)
        {
            list.Add(input[i + 1] - input[i]);
        }

        if (list.IsDistinct())
        {
            return list[^1];
        }
        else
        {
            return list[^1] + Calc(list);
        }

        throw new NotImplementedException();
    }
}
