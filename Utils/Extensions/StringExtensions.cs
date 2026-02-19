using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.Utils;

public static partial class StringExtensions
{
    extension(string s)
    {
        public int[] GetNumbers()
        {
            var matches = RegexGetNumbers().Matches(s);
            var array = new int[matches.Count];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(matches[i].Value);
            }

            return array;
        }

        public int[] GetUnsignedNumbers()
        {
            var matches = RegexGetUnsignedNumbers().Matches(s);
            var array = new int[matches.Count];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = int.Parse(matches[i].Value);
            }

            return array;
        }

        public long[] GetUnsignedLongNumbers()
        {
            var matches = RegexGetUnsignedNumbers().Matches(s);
            var array = new long[matches.Count];

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = long.Parse(matches[i].Value);
            }

            return array;
        }

        public int GetNumber(int offset = 0)
        {
            var length = 1;

            while (offset + length < s.Length && char.IsAsciiDigit(s[offset + length]))
            {
                length++;
            }

            return int.Parse(s[offset..(offset + length)]);
        }

        public int GetNumberWhitespace(int offset = 0)
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

        public List<T> ToList<T>(char seperator)
        {
            var split = s.Split(seperator);
            List<T> list = new(s.Length);
            var type = typeof(T);

            foreach (var item in split)
            {
                list.Add((T)Convert.ChangeType(item, type));
            }

            return list;
        }

        public T[] ToArray<T>(char seperator)
        {
            var split = s.Split(seperator);
            T[] array = new T[split.Length];
            var type = typeof(T);

            for (int i = 0; i < split.Length; i++)
            {
                array[i] = (T)Convert.ChangeType(split[i], type);
            }

            return array;
        }

        public List<T> ToSingleNumbers<T>()
        {
            List<T> list = new(s.Length);

            foreach (var character in s)
            {
                list.Add((T)Convert.ChangeType(character.ToNumber(), typeof(T)));
            }

            return list;
        }

        public bool IsLowerCase()
        {
            return s.All(char.IsLower);
        }

        public string Reverse()
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }

    [GeneratedRegex(@"(-?\d+)")]
    private static partial Regex RegexGetNumbers();

    [GeneratedRegex(@"(\d+)")]
    private static partial Regex RegexGetUnsignedNumbers();
}
