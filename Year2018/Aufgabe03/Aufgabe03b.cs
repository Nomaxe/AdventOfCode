using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe03b : IAufgabe
{
    private readonly GridInt _grid;
    private readonly string[] _input;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2018, 3);
        _grid = new(1000);
    }

    public string Calc()
    {
        HashSet<int> notOverlapping = [];

        foreach (var line in _input)
        {
            bool doesOverlap = false;
            var numbers = line.GetNumbers();

            for (int y = numbers[2]; y < numbers[2] + numbers[4]; y++)
            {
                for (int x = numbers[1]; x < numbers[1] + numbers[3]; x++)
                {
                    var value = _grid.GetValue(x, y);
                    if (value == 0)
                    {
                        _grid.SetValue(x, y, numbers[0]);
                    }
                    else
                    {
                        notOverlapping.Remove(value);
                        doesOverlap = true;
                    }
                }
            }

            if (!doesOverlap)
            {
                notOverlapping.Add(numbers[0]);
            }
        }

        return notOverlapping.First().ToString();
    }
}
