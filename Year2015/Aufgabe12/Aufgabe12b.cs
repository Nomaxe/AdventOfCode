using AdventOfCode.Utils;

namespace AdventOfCode.Year2015;

internal class Aufgabe12b : IAufgabe
{
    private readonly string _input;
    private int _position = 0;

    public Aufgabe12b()
    {
        _input = Utilities.ReadInput(2015, 12)[0];
    }

    public string Calc()
    {
        int sum = 0;

        for (; _position < _input.Length; _position++)
        {
            if (_input[_position] == '{')
            {
                CheckRed();
            }
            else if (char.IsAsciiDigit(_input[_position]) || _input[_position] == '-')
            {
                sum += GetNumber();
            }
        }

        return sum.ToString();
    }

    private void CheckRed()
    {
        int depth = 1;
        int arrayDepth = 0;
        int redCount = 0;
        bool hasRed = false;

        for (int i = _position + 1; i < _input.Length; i++)
        {
            switch (_input[i])
            {
                case '{':
                    depth++;
                    break;
                case '}':
                    depth--;
                    if (depth == 0)
                    {
                        if (hasRed)
                        {
                            _position = i;
                        }

                        return;
                    }
                    break;
                case '[':
                    arrayDepth++;
                    break;
                case ']':
                    arrayDepth--;
                    break;
                case 'r':
                    if (depth == 1 && arrayDepth == 0)
                    {
                        redCount = redCount == 0 ? 1 : 0;
                    }
                    break;
                case 'e':
                    if (depth == 1 && arrayDepth == 0)
                    {
                        redCount = redCount == 1 ? 2 : 0;
                    }
                    break;
                case 'd':
                    if (depth == 1 && arrayDepth == 0)
                    {
                        if (redCount == 2)
                        {
                            hasRed = true;
                        }
                        else
                        {
                            redCount = 0;
                        }
                    }
                    break;
                default:
                    redCount = 0;
                    break;
            }
        }
    }

    private int GetNumber()
    {
        int length = 1;

        while (char.IsAsciiDigit(_input[_position + length]))
        {
            length++;
        }

        var oldPosition = _position;
        _position += length;

        return int.Parse(_input[oldPosition..(oldPosition + length)]);
    }
}
