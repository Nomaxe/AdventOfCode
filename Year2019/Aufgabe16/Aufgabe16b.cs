using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe16b : IAufgabe
{
    private int[] _value;

    public Aufgabe16b()
    {
        var input = Utilities.ReadInputAsString(2019, 16).Select(x => x.ToNumber()).ToArray();
        int skip = 0;
        for (int i = 0; i < 7; i++)
        {
            skip *= 10;
            skip += input[i];
        }
        _value = Enumerable.Repeat(input, 10000).SelectMany(x => x).Skip(skip).ToArray();
    }

    public string Calc()
    {
        for (int i = 0; i < 100; i++)
        {
            CalcNextValue();
        }

        return string.Join("", _value.Take(8));
    }

    private void CalcNextValue()
    {
        long sum = 0;
        int[] newValue = new int[_value.Length];

        for (int i = 0; i < _value.Length; i++)
        {
            sum += _value[i];
        }

        for (int i = 0; i < _value.Length; i++)
        {
            newValue[i] = (int)(sum % 10);
            sum -= _value[i];
        }

        _value = newValue;
    }
}
