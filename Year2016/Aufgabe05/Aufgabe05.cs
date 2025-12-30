using AdventOfCode.Utils;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2016;

internal class Aufgabe05 : IAufgabe
{
    private readonly string _input;

    public Aufgabe05()
    {
        _input = Utilities.ReadInput(2016, 5)[0];
    }

    public string Calc()
    {
        StringBuilder builder = new(8);

        for (ulong i = 0; i < ulong.MaxValue; i++)
        {
            var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{_input}{i}"));
            if (hash[0] == 0 && hash[1] == 0)
            {
                var hashString = Convert.ToHexStringLower(hash);

                if (hashString[4] == '0')
                {
                    builder.Append(hashString[5]);

                    if (builder.Length == 8)
                    {
                        return builder.ToString();
                    }
                }
            }
        }

        throw new NotImplementedException();
    }
}
