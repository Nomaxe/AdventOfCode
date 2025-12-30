using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe02b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02b()
    {
        _input = Utilities.ReadInput(2021, 2);
    }

    public string Calc()
    {
        int horizontal = 0;
        int depth = 0;
        int aim = 0;

        foreach (var line in _input)
        {
            //we assume, that the numbers only have 1 digit
            int number = line[^1].ToNumber();

            switch (line[0])
            {
                case 'f':
                    horizontal += number;
                    depth += aim * number;
                    break;
                case 'u':
                    aim -= number;
                    break;
                case 'd':
                    aim += number;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return (horizontal * depth).ToString();
    }
}
