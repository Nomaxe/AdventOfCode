using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe14 : IAufgabe
{
    private readonly List<int> _list;
    private readonly int _skipValues;

    public Aufgabe14()
    {
        _skipValues = Utilities.ReadInputAsInt(2018, 14);
        _list = [3, 7];
    }

    public string Calc()
    {
        int index1 = 0;
        int index2 = 1;

        while (_list.Count < _skipValues + 10)
        {
            var value1 = _list[index1];
            var value2 = _list[index2];

            var sum = value1 + value2;

            if (sum >= 10)
            {
                _list.Add(sum / 10);
                _list.Add(sum % 10);
            }
            else
            {
                _list.Add(sum);
            }

            index1 = (index1 + 1 + value1) % _list.Count;
            index2 = (index2 + 1 + value2) % _list.Count;
        }

        return string.Join("", _list.Skip(_skipValues).Take(10));
    }
}
