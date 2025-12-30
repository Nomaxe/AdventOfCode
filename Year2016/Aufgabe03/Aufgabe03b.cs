using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe03b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe03b()
    {
        _input = Utilities.ReadInput(2016, 3);
    }

    public string Calc()
    {
        int result = 0;
        int[][] numberArray = [new int[3], new int[3], new int[3]];
        int index = 0;

        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();
            numberArray[0][index] = numbers[0];
            numberArray[1][index] = numbers[1];
            numberArray[2][index] = numbers[2];
            index++;

            if (index == 3)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (CheckTriangle(numberArray[i]))
                    {
                        result++;
                    }
                }
                index = 0;
            }
        }

        return result.ToString();
    }

    private static bool CheckTriangle(int[] numbers)
    {
        if (numbers[0] >= numbers[1])
        {
            if (numbers[0] >= numbers[2])
            {
                //0 big
                return numbers[1] + numbers[2] > numbers[0];
            }
            else
            {
                //2 big
                return numbers[0] + numbers[1] > numbers[2];
            }
        }
        else
        {
            if (numbers[1] >= numbers[2])
            {
                //1 big
                return numbers[0] + numbers[2] > numbers[1];
            }
            else
            {
                //2 big
                return numbers[0] + numbers[1] > numbers[2];
            }
        }
    }
}
