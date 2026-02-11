using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe04 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04()
    {
        _input = Utilities.ReadInput(2024, 4);
    }

    public string Calc()
    {
        int result = 0;

        //Horizontal
        foreach (var line in _input)
        {
            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Vertical
        for (int i = 0; i < _input.Length; i++)
        {
            string line = "";

            for (int j = 0; j < _input.Length; j++)
            {
                line += _input[j][i];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal oben rechts unten
        for (int i = 0; i < _input.Length; i++)
        {
            string line = "";

            for (int j = 0; i + j < _input.Length; j++)
            {
                line += _input[j][i + j];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal links rechts unten
        for (int i = 1; i < _input.Length; i++)
        {
            string line = "";

            for (int j = 0; i + j < _input.Length; j++)
            {
                line += _input[i + j][j];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal oben links unten
        for (int i = 0; i < _input.Length; i++)
        {
            string line = "";

            for (int j = 0; i - j >= 0; j++)
            {
                line += _input[j][i - j];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal rechts links unten
        for (int i = 1; i < _input.Length; i++)
        {
            string line = "";

            for (int j = 0; i + j < _input.Length; j++)
            {
                line += _input[i + j][_input.Length - j - 1];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }


        return result.ToString();
    }
}
