using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
    {
        _input = Utilities.ReadInput(2024, 2);
    }

    public string Calc()
    {
        List<int> numbers = [];
        int save = 0;
        foreach (var line in _input)
        {
            numbers.Clear();

            foreach (var number in line.Split(' '))
            {
                numbers.Add(int.Parse(number));
            }

            if (GetResult(numbers))
            {
                save++;
            }
        }

        return save.ToString();
    }

    private static bool GetResult(List<int> numbers)
    {
        bool increase;

        increase = numbers[1] > numbers[0];

        for (int i = 1; i < numbers.Count; i++)
        {
            if (increase)
            {
                if (numbers[i] <= numbers[i - 1])
                {
                    return false;
                }
            }
            else
            {
                if (numbers[i] >= numbers[i - 1])
                {
                    return false;
                }
            }

            if (Math.Abs(numbers[i] - numbers[i - 1]) < 1 || Math.Abs(numbers[i] - numbers[i - 1]) > 3)
            {
                return false;
            }
        }

        return true;
    }
}
