using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe10b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe10b()
    {
        _input = Utilities.ReadInput(2021, 10);
    }

    public string Calc()
    {
        List<ulong> points = [];

        foreach (var line in _input)
        {
            var point = CheckLine(line);
            if (point > 0)
            {
                points.Add(point);
            }
        }

        return points.Order().ToList()[points.Count / 2].ToString();
    }

    private static ulong CheckLine(string line)
    {
        List<char> openedChunks = [];

        foreach (var character in line)
        {
            switch (character)
            {
                case '(':
                case '[':
                case '{':
                case '<':
                    openedChunks.Add(character);
                    break;
                case ')':
                case ']':
                case '}':
                case '>':
                    if (IsCorrectChunk(openedChunks[^1], character))
                    {
                        openedChunks.RemoveAt(openedChunks.Count - 1);
                        continue;
                    }

                    return 0;
                default:
                    throw new NotImplementedException();
            }
        }

        if (openedChunks.Count > 0)
        {
            ulong result = 0;

            for (int i = openedChunks.Count - 1; i >= 0; i--)
            {
                result *= 5;

                result += openedChunks[i] switch
                {
                    '(' => 1,
                    '[' => 2,
                    '{' => 3,
                    '<' => 4,
                    _ => throw new NotImplementedException()
                };
            }

            return result;
        }

        return 0;
    }

    private static bool IsCorrectChunk(char open, char close)
    {
        return open switch
        {
            '(' => close == ')',
            '[' => close == ']',
            '{' => close == '}',
            '<' => close == '>',
            _ => throw new NotImplementedException()
        };
    }
}
