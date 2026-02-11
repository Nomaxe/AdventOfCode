using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe04b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04b()
    {
        _input = Utilities.ReadInput(2024, 4);
    }

    public string Calc()
    {
        int result = 0;

        for (int i = 0; i < _input.Length - 2; i++)
        {
            for (int j = 0; j < _input.Length - 2; j++)
            {
                char[,] array = new char[3, 3];
                array[0, 0] = _input[i][j];
                array[0, 1] = _input[i][j + 1];
                array[0, 2] = _input[i][j + 2];
                array[1, 0] = _input[i + 1][j];
                array[1, 1] = _input[i + 1][j + 1];
                array[1, 2] = _input[i + 1][j + 2];
                array[2, 0] = _input[i + 2][j];
                array[2, 1] = _input[i + 2][j + 1];
                array[2, 2] = _input[i + 2][j + 2];

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
