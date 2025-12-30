using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2018;

internal class Aufgabe02b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe02b()
    {
        _input = Utilities.ReadInput(2018, 2);
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i++)
        {
            for (int j = i + 1; j < _input.Length; j++)
            {
                if (IsCloseString(_input[i], _input[j]))
                {
                    return GetResultString(_input[i], _input[j]);
                }
            }
        }

        return "";
    }

    private static bool IsCloseString(string a, string b)
    {
        int differences = 0;

        //lengh is always the same
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i])
            {
                differences++;

                if (differences > 1)
                {
                    return false;
                }
            }
        }

        return true;
    }

    private static string GetResultString(string a, string b)
    {
        StringBuilder builder = new(a.Length - 1);

        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] == b[i])
            {
                builder.Append(a[i]);
            }
        }

        return builder.ToString();
    }
}
