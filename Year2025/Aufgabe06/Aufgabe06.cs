using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;
internal class Aufgabe06 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe06()
    {
        _input = Utilities.ReadInput(2025, 6);
    }

    public string Calc()
    {
        List<List<long>> numbers = new();
        long result = 0;

        for (int row = 0; row < _input.Length; row++)
        {
            if (row == 0)
            {
                var split = _input[row].GetUnsignedLongNumbers();
                for (int number = 0; number < split.Length; number++)
                {
                    numbers.Add([split[number]]);
                }
            }
            else if (row < _input.Length - 1)
            {
                var split = _input[row].GetUnsignedLongNumbers();
                for (int number = 0; number < split.Length; number++)
                {
                    numbers[number].Add(split[number]);
                }
            }
            else
            {
                var signs = _input[row].Where(x => x != ' ').ToList();

                for (int sign = 0; sign < signs.Count; sign++)
                {
                    long resultVertical = 0;
                    if (signs[sign] == '*')
                    {
                        resultVertical = 1;
                    }
                    foreach (var number in numbers[sign])
                    {
                        if (signs[sign] == '*')
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
            }
        }

        return result.ToString();
    }
}
