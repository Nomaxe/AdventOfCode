using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe10 : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe10()
    {
        _input = Utilities.ReadInput(2021, 10);
    }

    public string Calc()
    {
        int result = 0;

        foreach (var line in _input)
        {
            result += CheckLine(line);
        }

        return result.ToString();
    }

    private static int CheckLine(string line)
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

                    switch (character)
                    {
                        case ')':
                            return 3;
                        case ']':
                            return 57;
                        case '}':
                            return 1197;
                        case '>':
                            return 25137;
                    }
                    break;
                default:
                    throw new NotImplementedException();
            }
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
