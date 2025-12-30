using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe04 : IAufgabe
{
    public string Calc()
    {
        var input = Utilities.ReadInput(2024, 4);
        int result = 0;

        //Horizontal
        foreach (var line in input)
        {
            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Vertical
        for (int i = 0; i < input.Length; i++)
        {
            string line = "";

            for (int j = 0; j < input.Length; j++)
            {
                line += input[j][i];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal oben rechts unten
        for (int i = 0; i < input.Length; i++)
        {
            string line = "";

            for (int j = 0; i + j < input.Length; j++)
            {
                line += input[j][i + j];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal links rechts unten
        for (int i = 1; i < input.Length; i++)
        {
            string line = "";

            for (int j = 0; i + j < input.Length; j++)
            {
                line += input[i + j][j];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal oben links unten
        for (int i = 0; i < input.Length; i++)
        {
            string line = "";

            for (int j = 0; i - j >= 0; j++)
            {
                line += input[j][i - j];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }

        //Diagonal rechts links unten
        for (int i = 1; i < input.Length; i++)
        {
            string line = "";

            for (int j = 0; i + j < input.Length; j++)
            {
                line += input[i + j][input.Length - j - 1];
            }

            result += line.Split("XMAS").Length - 1;
            result += line.Split("SAMX").Length - 1;
        }


        return result.ToString();
    }
}
