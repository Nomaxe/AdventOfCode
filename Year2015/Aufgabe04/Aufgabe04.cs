using AdventOfCode.Utils;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2015;

internal class Aufgabe04 : IAufgabe
{
    private readonly string _key;

    public Aufgabe04()
    {
        _key = Utilities.ReadInput(2015, 4)[0];
    }

    public string Calc()
    {
        for (int i = 0; i < int.MaxValue; i++)
        {
            var hash = Convert.ToHexStringLower(MD5.HashData(Encoding.ASCII.GetBytes($"{_key}{i}")));
            if (hash[0] == '0' && hash[1] == '0' && hash[2] == '0' && hash[3] == '0' && hash[4] == '0')
            {
                return i.ToString();
            }
        }

        throw new NotImplementedException();
    }
}
