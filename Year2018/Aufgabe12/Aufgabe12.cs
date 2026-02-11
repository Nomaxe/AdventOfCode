using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe12 : IAufgabe
{
    private readonly string[] _input;
    private char[] _currentState;
    private readonly HashSet<string> _patterns;

    public Aufgabe12()
    {
        _input = Utilities.ReadInput(2018, 12);
        _currentState = _input[0][15..].ToCharArray();
        _patterns = new();
    }

    public string Calc()
    {
        const int Times = 20;
        const int Offset = 2 * Times;

        foreach (var line in _input.Skip(2).Where(x => x[9] == '#'))
        {
            _patterns.Add(line[..5]);
        }

        for (int i = 0; i < Times; i++)
        {
            var nextArray = new char[_currentState.Length + 4];

            for (int j = 0; j < nextArray.Length; j++)
            {
                nextArray[j] = GetCharacter(j);
            }

            _currentState = nextArray;
        }

        int count = 0;
        for (int i = 0; i < _currentState.Length; i++)
        {
            if (_currentState[i] == '#')
            {
                count += i - Offset;
            }
        }

        return count.ToString();
    }

    private char GetCharacter(int index)
    {
        string value;

        if (index == 0)
        {
            value = $"....{_currentState[0]}";
        }
        else if (index == 1)
        {
            value = $"...{_currentState[0]}{_currentState[1]}";
        }
        else if (index == 2)
        {
            value = $"..{_currentState[0]}{_currentState[1]}{_currentState[2]}";
        }
        else if (index == 3)
        {
            value = $".{_currentState[0]}{_currentState[1]}{_currentState[2]}{_currentState[3]}";
        }
        else if (index == _currentState.Length)
        {
            value = $"{_currentState[^4]}{_currentState[^3]}{_currentState[^2]}{_currentState[^1]}.";
        }
        else if (index == _currentState.Length + 1)
        {
            value = $"{_currentState[^3]}{_currentState[^2]}{_currentState[^1]}..";
        }
        else if (index == _currentState.Length + 2)
        {
            value = $"{_currentState[^2]}{_currentState[^1]}...";
        }
        else if (index == _currentState.Length + 3)
        {
            value = $"{_currentState[^1]}....";
        }
        else
        {
            value = $"{_currentState[index - 4]}{_currentState[index - 3]}{_currentState[index - 2]}{_currentState[index - 1]}{_currentState[index]}";
        }

        return _patterns.Contains(value) ? '#' : '.';
    }
}
