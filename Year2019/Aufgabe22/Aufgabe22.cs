using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe22 : IAufgabe
{
    private const int ArrayLength = 10007;
    private readonly string[] _input;

    public Aufgabe22()
    {
        _input = Utilities.ReadInput(2019, 22);
    }

    public string Calc()
    {
        int currentPosition = 2019;

        foreach (var line in _input)
        {
            if(line[0] == 'c') //cut
                {
                var number = int.Parse(line[4..]);

                if (number > 0)
                {
                    if (currentPosition < number)
                    {
                        currentPosition = ArrayLength - (number - currentPosition);
                    }
                    else
                    {
                        currentPosition -= number;
                    }
                }
                else
                {
                    number = -number;
                    var cutStart = ArrayLength - number;
                    if (currentPosition < cutStart)
                    {
                        currentPosition += number;
                    }
                    else
                    {
                        currentPosition -= ArrayLength - number;
                    }
                }
            }
                else if (line[5] == 'i') //deal into new stack
            {
                currentPosition = ArrayLength - currentPosition - 1;
            }
            else //deal with increment
            {
                var number = int.Parse(line[20..]);
                currentPosition = currentPosition * number % ArrayLength;
            }
        }

        return currentPosition.ToString();
    }
}
