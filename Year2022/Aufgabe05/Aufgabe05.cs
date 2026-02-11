using AdventOfCode.Utils;

namespace AdventOfCode.Year2022;

internal class Aufgabe05 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<string> _numbers;
    private readonly Dictionary<int, List<char>> _stacks;

    public Aufgabe05()
    {
        _input = Utilities.ReadInput(2022, 5);
        _numbers = new();
        _stacks = new(9);
    }

    public string Calc()
    {
        for (int i = 1; i <= 9; i++)
        {
            _stacks.Add(i, []);
        }

        var whiteline = Array.IndexOf(_input, string.Empty);

        for (int i = whiteline - 2; i >= 0; i--)
        {
            var index = 1;
            for (int j = 1; j <= 9; j++)
            {
                if (_input[i][index] != ' ')
                {
                    _stacks[j].Add(_input[i][index]);
                }

                index += 4;
            }
        }

        _numbers.AddRange(_input.Skip(whiteline + 1));

        foreach (var line in _numbers)
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
