using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe14b : IAufgabe
{
    private readonly List<int> _list;
    private readonly List<int> _goalNumbers;

    public Aufgabe14b()
    {
        _goalNumbers = Utilities.ReadInputAsString(2018, 14).Select(x => x.ToNumber()).ToList();
        _list = [3, 7];
    }

    public string Calc()
    {
        int index1 = 0;
        int index2 = 1;

        while (true)
        {
            var value1 = _list[index1];
            var value2 = _list[index2];

            var sum = value1 + value2;

            if (sum >= 10)
            {
                _list.Add(sum / 10);
                if (Check())
                {
                    break;
                }
                _list.Add(sum % 10);
            }
            else
            {
                _list.Add(sum);
            }
            if (Check())
            {
                break;
            }

            index1 = (index1 + 1 + value1) % _list.Count;
            index2 = (index2 + 1 + value2) % _list.Count;
        }

        return (_list.Count - _goalNumbers.Count).ToString();
    }

    private bool Check()
    {
        for (int i = 1; i <= _goalNumbers.Count; i++)
        {
            if (_goalNumbers[^i] != _list[^i])
            {
                return false;
            }
        }

        return true;
    }
}
