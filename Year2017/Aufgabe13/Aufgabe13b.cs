using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe13b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe13b()
    {
        _input = Utilities.ReadInput(2017, 13);
    }

    public string Calc()
    {
        Dictionary<int, int> steps = new(_input.Length);
        foreach (var line in _input)
        {
            var split = line.Split(": ");
            steps.Add(int.Parse(split[0]), (int.Parse(split[1]) - 1) * 2);
        }

        for (int i = 0; i < int.MaxValue; i++)
        {
            if (steps.All(x => (x.Key + i) % x.Value != 0))
            {
                return i.ToString();
            }
        }

        throw new NotImplementedException();
    }
}
