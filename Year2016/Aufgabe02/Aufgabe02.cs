using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe02 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02()
    {
        _input = Utilities.ReadInput(2016, 2);
    }

    public string Calc()
    {
        string result = string.Empty;
        int currentNumber = 5;

        foreach (var line in _input)
        {
            foreach (var character in line)
            {
                switch (character)
                {
                    case 'U':
                        if (currentNumber > 3)
                        {
                            currentNumber -= 3;
                        }
                        break;
                    case 'R':
                        if (currentNumber % 3 != 0)
                        {
                            currentNumber++;
                        }
                        break;
                    case 'D':
                        if (currentNumber <= 7)
                        {
                            currentNumber += 3;
                        }
                        break;
                    case 'L':
                        if (currentNumber % 3 != 1)
                        {
                            currentNumber--;
                        }
                        break;
                }
            }

            result += currentNumber;
        }

        return result;
    }
}
