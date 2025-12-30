using System.Text.RegularExpressions;

namespace AdventOfCode.Utils;

public static partial class StringExtensions
{
    public static int[] GetNumbers(this string s)
    {
        var matches = RegexGetNumbers().Matches(s);
        var array = new int[matches.Count];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(matches[i].Value);
        }

        return array;
    }

    public static int[] GetUnsignedNumbers(this string s)
    {
        var matches = RegexGetUnsignedNumbers().Matches(s);
        var array = new int[matches.Count];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = int.Parse(matches[i].Value);
        }

        return array;
    }

    public static long[] GetUnsignedLongNumbers(this string s)
    {
        var matches = RegexGetUnsignedNumbers().Matches(s);
        var array = new long[matches.Count];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = long.Parse(matches[i].Value);
        }

        return array;
    }

    public static int GetNumber(this string s, int offset = 0)
    {
        var length = 1;

        while (offset + length < s.Length && char.IsAsciiDigit(s[offset + length]))
        {
            length++;
        }

        return int.Parse(s[offset..(offset + length)]);
    }

    public static int GetNumberWhitespace(this string s, int offset = 0)
    {
        var length = 1;

        while (s[offset + length] == ' ')
        {
            offset++;
        }

        while (char.IsAsciiDigit(s[offset + length]))
        {
            length++;
        }

        return int.Parse(s[offset..(offset + length)]);
    }

    public static List<int> ToIntList(this string s)
    {
        var split = s.Split(',');
        List<int> list = new(s.Length);
        foreach (var item in split)
        {
            list.Add(int.Parse(item));
        }

        return list;
    }

    public static bool IsLowerCase(this string s)
    {
        return s.All(char.IsLower);
    }


    [GeneratedRegex(@"(-?\d+)")]
    private static partial Regex RegexGetNumbers();

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex RegexGetUnsignedNumbers();
}
