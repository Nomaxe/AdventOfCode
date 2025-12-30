using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe04b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe04b()
    {
        _input = Utilities.ReadInput(2016, 4);
    }

    public string Calc()
    {
        const int Alphabet = 'z' - 'a' + 1;

        foreach (var line in _input)
        {
            var split = line[..^7].Split('-');
            int id = int.Parse(split[^1]);
            int move = id % 26;

            var array = line[..^7].ToCharArray();

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == '-' || char.IsNumber(array[i]))
                {
                    continue;
                }

                array[i] = (char)(array[i] + move);
                if (array[i] > 'z')
                {
                    array[i] = (char)(array[i] - Alphabet);
                }
            }

            if (new string(array).StartsWith("northpole-object-storage"))
            {
                return id.ToString();
            }
        }

        throw new NotImplementedException();
    }
}
