using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe24 : IAufgabe
{
    private readonly int[] _input;
    private readonly int _groupSum;
    private ulong? _result;

    public Aufgabe24()
    {
        _input = Utilities.ReadInputAsIntArray(2015, 24);
        _groupSum = _input.Sum() / 3;
    }

    public string Calc()
    {
        for (int groupSize = GetStartGroupSize(); groupSize < _input.Length; groupSize++)
        {
            Check(_groupSum, 0, groupSize, []);

            if (_result.HasValue)
            {
                return _result.Value.ToString();
            }
        }

        throw new NotImplementedException();
    }

    private void Check(int remaining, int index, int groupSize, HashSet<int> groupNumbers)
    {
        if (groupNumbers.Count == groupSize && remaining == 0)
        {
            //Rätsel ist so aufgebaut, dass die anderen Gruppen immer gültig sind, wenn die erste passt
            if (_result.HasValue)
            {
                _result = ulong.Min(_result.Value, GetResult(groupNumbers));
            }
            else
            {
                _result = GetResult(groupNumbers);
            }

            return;
        }

        for (int i = index; i < _input.Length; i++)
        {
            var nextRemaining = remaining - _input[i];

            if (nextRemaining < 0)
            {
                return;
            }

            HashSet<int> nextGroupNumbers = [.. groupNumbers];
            nextGroupNumbers.Add(_input[i]);

            Check(nextRemaining, i + 1, groupSize, nextGroupNumbers);
        }
    }

    private int GetStartGroupSize()
    {
        int sum = 0;

        for (int i = _input.Length - 1; i >= 0; i--)
        {
            sum += _input[i];

            if (sum >= _groupSum)
            {
                return _input.Length - i;
            }
        }

        throw new NotImplementedException();
    }

    private static ulong GetResult(HashSet<int> groupNumbers)
    {
        ulong result = 1;

        foreach (var number in groupNumbers)
        {
            result *= (ulong)number;
        }

        return result;
    }
}
