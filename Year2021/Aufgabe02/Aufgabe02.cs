using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
    {
        _input = Utilities.ReadInput(2021, 2);
    }

    public string Calc()
    {
        int horizontal = 0;
        int depth = 0;

        foreach (var line in _input)
        {
            //we assume, that the numbers only have 1 digit
            int number = line[^1].ToNumber();

            switch (line[0])
            {
                case 'f':
                    horizontal += number;
                    break;
                case 'u':
                    depth -= number;
                    break;
                case 'd':
                    depth += number;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return (horizontal * depth).ToString();
    }
}
