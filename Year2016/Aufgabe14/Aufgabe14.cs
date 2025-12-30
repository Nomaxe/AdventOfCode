using AdventOfCode.Utils;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode.Year2016;

internal partial class Aufgabe14 : IAufgabe
{
    private readonly string _salt;
    private long _index = 0;

    public Aufgabe14()
    {
        _salt = Utilities.ReadInput(2016, 14)[0];
    }

    public string Calc()
    {
        SortedSet<long> validIndex = [];
        DictionaryHashSet<char, long> openedHashes = [];
        long upToIndex = long.MaxValue;

        do
        {
            var hash = Convert.ToHexString(MD5.HashData(Encoding.ASCII.GetBytes($"{_salt}{_index}")));
            var char5 = Char5().Match(hash);
            if (char5.Success)
            {
                openedHashes.RemoveWhere(char5.Value[0], x => x < _index - 1000, false);
                foreach (var index in openedHashes[char5.Value[0]])
                {
                    validIndex.Add(index);
                }

                if (validIndex.Count >= 64 && upToIndex == long.MaxValue)
                {
                    upToIndex = _index + 1000;
                }

                openedHashes.RemoveAll(char5.Value[0]);
            }

            var char3 = Char3().Match(hash);
            if (char3.Success)
            {
                openedHashes.Add(char3.Value[0], _index);
            }

            _index++;
        } while (_index <= upToIndex);

        return validIndex.ElementAt(63).ToString();
    }

    [GeneratedRegex(@"(.)\1{4}")]
    private static partial Regex Char5();
    [GeneratedRegex(@"(.)\1{2}")]
    private static partial Regex Char3();
}
