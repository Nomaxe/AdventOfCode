using AdventOfCode.Utils;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2016;

internal class Aufgabe05b : IAufgabe
{
    private readonly string _input;

    public Aufgabe05b()
    {
        _input = Utilities.ReadInput(2016, 5)[0];
    }

    public string Calc()
    {
        char[] password = new char[8];
        HashSet<int> alreadyFilled = new(8);

        for (ulong i = 0; i < ulong.MaxValue; i++)
        {
            var hash = MD5.HashData(Encoding.ASCII.GetBytes($"{_input}{i}"));
            if (hash[0] == 0 && hash[1] == 0)
            {
                var hashString = Convert.ToHexStringLower(hash);

                if (hashString[4] == '0')
                {
                    var position = hashString[5].ToNumber();

                    if (position >= 0 && position < 8)
                    {
                        if (!alreadyFilled.Contains(position))
                        {
                            password[position] = hashString[6];
                            alreadyFilled.Add(position);

                            if (alreadyFilled.Count == 8)
                            {
                                return new(password);
                            }
                        }
                    }
                }
            }
        }

        throw new NotImplementedException();
    }
}
