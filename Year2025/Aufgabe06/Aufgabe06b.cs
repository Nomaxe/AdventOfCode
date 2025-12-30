using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;
internal class Aufgabe06b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe06b()
    {
        _input = Utilities.ReadInput(2025, 6);
    }

    public string Calc()
    {
        var signs = GetIndexesOfSigns();
        long result = 0;

        for (int i = 0; i < signs.Count; i++)
        {
            var signIndex = signs[i];
            var readUtil = ReadUntil(i, signs);
            long resultVertical;

            if (_input[^1][signIndex] == '*')
            {
                resultVertical = 1;
            }
            else
            {
                resultVertical = 0;
            }

            for (int index = readUtil; index >= signIndex; index--)
            {
                long number = 0;

                for (int row = 0; row < _input.Length - 1; row++)
                {
                    if (_input[row][index] == ' ')
                    {
                        continue;
                    }

                    number *= 10;
                    number += _input[row][index] - '0';
                }

                if (_input[^1][signIndex] == '*')
                {
                    resultVertical *= number;
                }
                else
                {
                    resultVertical += number;
                }
            }

            result += resultVertical;
        }

        return result.ToString();
    }

    private List<int> GetIndexesOfSigns()
    {
        List<int> signs = new();

        for (int i = 0; i < _input[^1].Length; i++)
        {
            if (_input[^1][i] == '*' || _input[^1][i] == '+')
            {
                signs.Add(i);
            }
        }

        return signs;
    }

    private int ReadUntil(int currentIndex, List<int> signs)
    {
        if (currentIndex < signs.Count - 1)
        {
            return signs[currentIndex + 1] - 2;
        }

        return _input.Max(x => x.Length - 1);
    }
}
