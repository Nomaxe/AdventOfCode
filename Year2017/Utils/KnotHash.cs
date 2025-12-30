using System.Text;

namespace AdventOfCode.Year2017.Utils;

internal class KnotHash
{
    public IReadOnlyList<int> List => _list.AsReadOnly();

    private readonly List<int> _lengths;
    private readonly int[] _list;
    private readonly int _times;

    private const int ArraySize = 256;

    public KnotHash(List<int> input) : this(input, 1)
    {

    }

    public KnotHash(string input) : this([.. input.Select(x => (int)x), 17, 31, 73, 47, 23], 64)
    {

    }

    private KnotHash(List<int> input, int times)
    {
        _lengths = input;
        _times = times;
        _list = new int[ArraySize];

        for (int i = 1; i < ArraySize; i++)
        {
            _list[i] = i;
        }
    }

    public void Calc()
    {
        int currentPosition = 0;
        int skip = 0;

        for (int i = 0; i < _times; i++)
        {
            foreach (var length in _lengths)
            {
                var start = currentPosition;
                var end = currentPosition + length - 1;

                while (start < end)
                {
                    var indexStart = start % ArraySize;
                    var indexEnd = end % ArraySize;

                    (_list[indexEnd], _list[indexStart]) = (_list[indexStart], _list[indexEnd]);

                    start++;
                    end--;
                }

                currentPosition += length + skip;
                currentPosition %= ArraySize;
                skip++;
            }
        }
    }

    public string GetResult()
    {
        StringBuilder builder = new(32);
        for (int i = 0; i < ArraySize; i += 16)
        {
            var result = _list[i] ^ _list[i + 1] ^ _list[i + 2] ^ _list[i + 3] ^ _list[i + 4] ^ _list[i + 5] ^ _list[i + 6] ^ _list[i + 7] ^
                         _list[i + 8] ^ _list[i + 9] ^ _list[i + 10] ^ _list[i + 11] ^ _list[i + 12] ^ _list[i + 13] ^ _list[i + 14] ^ _list[i + 15];

            builder.Append(result.ToString("x2"));
        }

        return builder.ToString();
    }
}
