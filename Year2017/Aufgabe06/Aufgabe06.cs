using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe06 : IAufgabe
{
    private readonly List<int> _input;
    private readonly HashSet<string> _loopChecker = [];

    public Aufgabe06()
    {
        _input = Utilities.ReadInput(2017, 6)[0].Split('\t').Select(int.Parse).ToList();
    }

    public string Calc()
    {
        int count = 0;
        string currentValue = string.Join(',', _input);

        do
        {
            _loopChecker.Add(currentValue);

            int index = GetMaxIndex();
            int value = _input[index];
            int remaining = _input.Count;
            for (int i = 1; i < _input.Count; i++)
            {
                var number = (int)double.Round((double)value / remaining, 0, MidpointRounding.ToPositiveInfinity);
                _input[(index + i) % _input.Count] += number;
                remaining--;
                value -= number;
            }
            _input[index] = value;

            count++;
            currentValue = string.Join(',', _input);
        } while (!_loopChecker.Contains(currentValue));

        return count.ToString();
    }

    private int GetMaxIndex()
    {
        int index = 0;
        int max = 0;

        for (int i = 0; i < _input.Count; i++)
        {
            if (_input[i] > max)
            {
                max = _input[i];
                index = i;
            }
        }

        return index;
    }
}
