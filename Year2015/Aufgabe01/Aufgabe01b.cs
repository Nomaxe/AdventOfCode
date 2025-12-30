using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe01b : IAufgabe
{
    private readonly string _input;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInput(2015, 1)[0];
    }

    public string Calc()
    {
        int floor = 0;

        for (int i = 0; i < _input.Length; i++)
        {
            switch (_input[i])
            {
                case '(':
                    floor++;
                    break;
                case ')':
                    floor--;
                    if (floor < 0)
                    {
                        return (i + 1).ToString();
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        throw new NotImplementedException();
    }
}
