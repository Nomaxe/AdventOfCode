using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe09b : IAufgabe
{
    private readonly char[] _input;
    private readonly List<int> _disk;
    private int _currentFileId;
    private ulong _result;

    public Aufgabe09b()
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

        _currentFileId = fileId - 1;

        int indexOfFileId, count, indexOfSpace;

        while (_currentFileId > 0)
        {
            indexOfFileId = GetIndexOfFileId();
            count = GetCountOfFileId(indexOfFileId);
            indexOfSpace = GetEmptySpace(count);

            if (indexOfSpace > indexOfFileId)
            {
                _currentFileId--;
                continue;
            }

            for (int i = 0; i < count; i++)
            {
                _disk[indexOfSpace + i] = _disk[indexOfFileId + i];
                _disk[indexOfFileId + i] = -1;
            }

            _currentFileId--;
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

    private int GetIndexOfFileId()
    {
        for (int i = 0; i < _disk.Count; i++)
        {
            if (_disk[i] == _currentFileId)
            {
                return i;
            }
        }

        return -1;
    }

    private int GetCountOfFileId(int index)
    {
        int count = 1;

        for (int i = index + 1; i < _disk.Count; i++)
        {
            if (_disk[i] == _currentFileId)
            {
                count++;
            }
            else
            {
                break;
            }
        }

        return count;
    }

    private int GetEmptySpace(int amount)
    {
        int currentAmount = 0;

        for (int i = 0; i <= _disk.Count; i++)
        {
            if (_disk[i] == -1)
            {
                currentAmount++;

                if (currentAmount == amount)
                {
                    return i - amount + 1;
                }
            }
            else
            {
                currentAmount = 0;
            }
        }

        return -1;
    }

    private static int GetNumericValue(char character)
    {
        return Convert.ToInt32(char.GetNumericValue(character));
    }
}
