using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe21 : IAufgabe
{
    private readonly string[] _input;
    private readonly char[] _password;

    public Aufgabe21()
    {
        _input = Utilities.ReadInput(2016, 21);
        _password = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h'];
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            if (line.StartsWith("swap p"))
            {
                SwapPosition(line);
            }
            else if (line.StartsWith("swap l"))
            {
                SwapLetter(line);
            }
            else if (line.StartsWith("rotate l"))
            {
                RotateLeft(line);
            }
            else if (line.StartsWith("rotate r"))
            {
                RotateRight(line);
            }
            else if (line.StartsWith("rotate b"))
            {
                RotateBased(line);
            }
            else if (line.StartsWith("reverse"))
            {
                Reverse(line);
            }
            else if (line.StartsWith("move"))
            {
                Move(line);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        return new(_password);
    }

    private void SwapPosition(string line)
    {
        var numbers = line.GetUnsignedNumbers();
        (_password[numbers[0]], _password[numbers[1]]) = (_password[numbers[1]], _password[numbers[0]]);
    }

    private void SwapLetter(string line)
    {
        var index1 = Array.IndexOf(_password, line[12]);
        var index2 = Array.IndexOf(_password, line[26]);

        (_password[index1], _password[index2]) = (_password[index2], _password[index1]);
    }

    private void RotateLeft(string line)
    {
        var steps = line[12].ToNumber();
        var copy = new char[_password.Length];
        Array.Copy(_password, copy, _password.Length);

        for (int i = 0; i < _password.Length; i++)
        {
            var copyIndex = (i + steps) % _password.Length;

            _password[i] = copy[copyIndex];
        }
    }

    private void RotateRight(string line)
    {
        RotateRight(line[13].ToNumber());
    }

    private void RotateRight(int steps)
    {
        var copy = new char[_password.Length];
        Array.Copy(_password, copy, _password.Length);

        for (int i = 0; i < _password.Length; i++)
        {
            var copyIndex = i - steps;
            while (copyIndex < 0)
            {
                copyIndex += _password.Length;
            }

            _password[i] = copy[copyIndex];
        }
    }

    private void RotateBased(string line)
    {
        var index = Array.IndexOf(_password, line[^1]);
        var steps = 1 + index;
        if (index >= 4)
        {
            steps++;
        }

        RotateRight(steps);
    }

    private void Reverse(string line)
    {
        var start = line[18].ToNumber();
        var length = line[28].ToNumber() - start + 1;

        Array.Reverse(_password, start, length);
    }

    private void Move(string line)
    {
        var index1 = line[14].ToNumber();
        var index2 = line[28].ToNumber();

        var character = _password[index1];
        for (int i = index1; i < _password.Length - 1; i++)
        {
            _password[i] = _password[i + 1];
        }

        for (int i = _password.Length - 2; i >= index2; i--)
        {
            _password[i + 1] = _password[i];
        }
        _password[index2] = character;
    }
}
