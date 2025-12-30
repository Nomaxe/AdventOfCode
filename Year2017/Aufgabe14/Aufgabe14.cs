using AdventOfCode.Utils;
using AdventOfCode.Year2017.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe14 : IAufgabe
{
    private readonly string _input;

    public Aufgabe14()
    {
        _input = Utilities.ReadInput(2017, 14)[0];
    }

    public string Calc()
    {
        int count = 0;

        for (int i = 0; i < 128; i++)
        {
            KnotHash hash = new($"{_input}-{i}");
            hash.Calc();
            var result = hash.GetResult();

            foreach (var character in result)
            {
                switch (character)
                {
                    case 'f':
                        count += 4;
                        break;
                    case 'e':
                    case 'd':
                    case 'b':
                    case '7':
                        count += 3;
                        break;
                    case 'c':
                    case 'a':
                    case '9':
                    case '6':
                    case '5':
                    case '3':
                        count += 2;
                        break;
                    case '8':
                    case '4':
                    case '2':
                    case '1':
                        count += 1;
                        break;
                }
            }
        }

        return count.ToString();
    }
}
