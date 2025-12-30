using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe04b : IAufgabe
{
    public string Calc()
    {
        var input = Utilities.ReadInput(2024, 4);
        int result = 0;

        for (int i = 0; i < input.Length - 2; i++)
        {
            for (int j = 0; j < input.Length - 2; j++)
            {
                char[,] array = new char[3, 3];
                array[0, 0] = input[i][j];
                array[0, 1] = input[i][j + 1];
                array[0, 2] = input[i][j + 2];
                array[1, 0] = input[i + 1][j];
                array[1, 1] = input[i + 1][j + 1];
                array[1, 2] = input[i + 1][j + 2];
                array[2, 0] = input[i + 2][j];
                array[2, 1] = input[i + 2][j + 1];
                array[2, 2] = input[i + 2][j + 2];

                if (IsXMas(array))
                {
                    result++;
                }
            }
        }

        return result.ToString();
    }

    private static bool IsXMas(char[,] array)
    {
        if (array[0, 0] == 'M' && array[1, 1] == 'A' && array[2, 2] == 'S' || array[0, 0] == 'S' && array[1, 1] == 'A' && array[2, 2] == 'M')
        {
            if (array[0, 2] == 'M' && array[1, 1] == 'A' && array[2, 0] == 'S' || array[0, 2] == 'S' && array[1, 1] == 'A' && array[2, 0] == 'M')
            {
                return true;
            }
        }

        return false;
    }
}
