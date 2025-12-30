using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe11 : IAufgabe
{
    private List<ulong> _currentList;

    public Aufgabe11()
    {
        var input = Utilities.ReadInput(2024, 11);
        var split = input[0].Split(' ');
        _currentList = new(split.Length);
        foreach (var number in split)
        {
            _currentList.Add(ulong.Parse(number));
        }
    }

    public string Calc()
    {
        for (int i = 0; i < 25; i++)
        {
            List<ulong> nextList = new(_currentList.Count * 2);

            foreach (var number in _currentList)
            {
                if (number == 0)
                {
                    nextList.Add(1);
                    continue;
                }

                var text = number.ToString();
                if (text.Length % 2 == 0)
                {
                    nextList.Add(ulong.Parse(text[..(text.Length / 2)]));
                    nextList.Add(ulong.Parse(text[(text.Length / 2)..]));
                }
                else
                {
                    nextList.Add(number * 2024);
                }
            }

            _currentList = nextList;
        }

        return _currentList.Count.ToString();
    }
}
