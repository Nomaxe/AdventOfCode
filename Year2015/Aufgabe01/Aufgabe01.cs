using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe01 : IAufgabe
{
    private readonly string _input;

    public Aufgabe01()
    {
        _input = Utilities.ReadInput(2015, 1)[0];
    }

    public string Calc()
    {
        int floor = 0;

        foreach (var character in _input)
        {
            switch (character)
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        return floor.ToString();
    }
}
