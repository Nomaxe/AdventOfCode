using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe12b : IAufgabe
{
    public char[] _input;
    public HashSet<string> _patterns;

    public Aufgabe12b()
    {
        var input = Utilities.ReadInput(2018, 12);
        _input = input[0][15..].ToCharArray();
        _patterns = new();
        foreach (var line in input.Skip(2).Where(x => x[9] == '#'))
        {
            _patterns.Add(line[..5]);
        }
    }

    public string Calc()
    {
        const long Times = 50000000000;
        long currentOffset = 0;

        for (long i = 0; i < Times; i++)
        {
            var nextArray = new char[_input.Length + 4];

            for (int j = 0; j < nextArray.Length; j++)
            {
                nextArray[j] = GetCharacter(j);
            }

            var firstIndex = Array.IndexOf(nextArray, '#');
            var lastIndex = Array.LastIndexOf(nextArray, '#');
            var nextArrayTrim = new char[lastIndex - firstIndex + 1];
            Array.Copy(nextArray, firstIndex, nextArrayTrim, 0, lastIndex - firstIndex + 1);


            if (_input.SequenceEqual(nextArrayTrim))
            {
                currentOffset += (firstIndex - 2) * (Times - i);
                break;
            }

            currentOffset += firstIndex - 2;

            _input = nextArrayTrim;
        }

        long count = 0;
        for (int i = 0; i < _input.Length; i++)
        {
            if (_input[i] == '#')
            {
                count += i + currentOffset;
            }
        }

        return count.ToString();

        //>5335
    }

    private char GetCharacter(int index)
    {
        string value;

        if (index == 0)
        {
            value = $"....{_input[0]}";
        }
        else if (index == 1)
        {
            value = $"...{_input[0]}{_input[1]}";
        }
        else if (index == 2)
        {
            value = $"..{_input[0]}{_input[1]}{_input[2]}";
        }
        else if (index == 3)
        {
            value = $".{_input[0]}{_input[1]}{_input[2]}{_input[3]}";
        }
        else if (index == _input.Length)
        {
            value = $"{_input[^4]}{_input[^3]}{_input[^2]}{_input[^1]}.";
        }
        else if (index == _input.Length + 1)
        {
            value = $"{_input[^3]}{_input[^2]}{_input[^1]}..";
        }
        else if (index == _input.Length + 2)
        {
            value = $"{_input[^2]}{_input[^1]}...";
        }
        else if (index == _input.Length + 3)
        {
            value = $"{_input[^1]}....";
        }
        else
        {
            value = $"{_input[index - 4]}{_input[index - 3]}{_input[index - 2]}{_input[index - 1]}{_input[index]}";
        }

        return _patterns.Contains(value) ? '#' : '.';
    }
}
