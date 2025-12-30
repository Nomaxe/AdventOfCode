using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe03 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03()
    {
        _input = Utilities.ReadInput(2016, 3);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();

            if (numbers[0] >= numbers[1])
            {
                if (numbers[0] >= numbers[2])
                {
                    //0 big
                    if (numbers[1] + numbers[2] > numbers[0])
                    {
                        result++;
                    }
                }
                else
                {
                    //2 big
                    if (numbers[0] + numbers[1] > numbers[2])
                    {
                        result++;
                    }
                }
            }
            else
            {
                if (numbers[1] >= numbers[2])
                {
                    //1 big
                    if (numbers[0] + numbers[2] > numbers[1])
                    {
                        result++;
                    }
                }
                else
                {
                    //2 big
                    if (numbers[0] + numbers[1] > numbers[2])
                    {
                        result++;
                    }
                }
            }
        }

        return result.ToString();
    }
}
