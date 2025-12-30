using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe05 : IAufgabe
{
    private readonly string[] _input;
    private readonly Dictionary<int, List<char>> _stacks;

    public Aufgabe05()
    {
        _stacks = new(9);
        for (int i = 1; i <= 9; i++)
        {
            _stacks.Add(i, []);
        }

        var input = Utilities.ReadInput(2022, 5);
        var whiteline = Array.IndexOf(input, string.Empty);

        for (int i = whiteline - 2; i >= 0; i--)
        {
            var index = 1;
            for (int j = 1; j <= 9; j++)
            {
                if (input[i][index] != ' ')
                {
                    _stacks[j].Add(input[i][index]);
                }

                index += 4;
            }
        }

        _input = input.Skip(whiteline + 1).ToArray();
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var numbers = line.GetUnsignedNumbers();
            //0 = amount
            //1 = from
            //2 = to

            var fromList = _stacks[numbers[1]];
            var toList = _stacks[numbers[2]];
            for (int i = 0; i < numbers[0]; i++)
            {
                toList.Add(fromList[^1]);
                fromList.RemoveAt(fromList.Count - 1);
            }
        }

        return string.Join("", _stacks.Values.Select(x => x[^1]));
    }
}
