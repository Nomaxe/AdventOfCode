using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe09 : IAufgabe
{
    private readonly char[] _input;
    private readonly List<int> _disk;
    private ulong _result;

    public Aufgabe09()
    {
        var input = Utilities.ReadInput(2024, 9);
        _input = input[0].ToCharArray();
        _disk = new(_input.Length);
    }

    public string Calc()
    {
        int fileId = 0;
        AddToDisk(0, GetNumericValue(_input[0]));
        fileId++;
        for (int i = 1; i < _input.Length; i += 2)
        {
            AddToDisk(-1, GetNumericValue(_input[i]));
            AddToDisk(fileId, GetNumericValue(_input[i + 1]));
            fileId++;
        }

        int firstIndex, lastIndex;

        firstIndex = GetFirstIndex();
        lastIndex = GetLastIndex();

        while (firstIndex < lastIndex)
        {
            _disk[firstIndex] = _disk[lastIndex];
            _disk[lastIndex] = -1;
            firstIndex = GetFirstIndex();
            lastIndex = GetLastIndex();
        }

        for (int i = 0; i < _disk.Count; i++)
        {
            if (_disk[i] == -1)
            {
                continue;
            }

            _result += Convert.ToUInt64(_disk[i] * i);
        }

        return _result.ToString();
    }

    private void AddToDisk(int number, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _disk.Add(number);
        }
    }

    private int GetFirstIndex()
    {
        for (int i = 0; i <= _disk.Count; i++)
        {
            if (_disk[i] == -1)
            {
                return i;
            }
        }

        return -1;
    }

    private int GetLastIndex()
    {
        for (int i = _disk.Count - 1; i >= 0; i--)
        {
            if (_disk[i] != -1)
            {
                return i;
            }
        }

        return -1;
    }

    private static int GetNumericValue(char character)
    {
        return Convert.ToInt32(char.GetNumericValue(character));
    }
}
