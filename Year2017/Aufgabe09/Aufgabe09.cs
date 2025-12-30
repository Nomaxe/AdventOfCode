using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe09 : IAufgabe
{
    private readonly string _input;

    public Aufgabe09()
    {
        _input = Utilities.ReadInput(2017, 9)[0];
    }

    public string Calc()
    {
        int count = 0;
        int depth = 0;
        bool garbage = false;

        for (int i = 0; i < _input.Length; i++)
        {
            if (_input[i] == '!')
            {
                i++;
                continue;
            }

            if (!garbage)
            {
                switch (_input[i])
                {
                    case '{':
                        depth++;
                        count += depth;
                        break;
                    case '}':
                        depth--;
                        break;
                    case '<':
                        garbage = true;
                        break;
                }
                ;
            }
            else
            {
                if (_input[i] == '>')
                {
                    garbage = false;
                }
            }
        }

        return count.ToString();
    }
}
