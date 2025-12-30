namespace AdventOfCode.Utils;

internal static class Utilities
{
    public static string[] ReadInput(int year, int day)
    {
        return File.ReadAllLines($@"..\..\..\Year{year}\Dateien\{day}.txt");
    }

    public static string ReadInputAsString(int year, int day)
    {
        return ReadInput(year, day)[0];
    }

    public static int[] ReadInputAsIntArray(int year, int day)
    {
        var input = ReadInput(year, day);

        var array = new int[input.Length];
        for (int i = 0; i < input.Length; i++)
        {
            array[i] = int.Parse(input[i]);
        }

        return array;
    }

    public static List<int> ReadInputAsIntList(int year, int day, char seperator = ',')
    {
        var input = ReadInput(year, day);

        var split = input[0].Split(seperator);
        List<int> list = new(split.Length);

        foreach (var number in split)
        {
            list.Add(int.Parse(number));
        }

        return list;
    }

    public static HashSet<int> ReadInputAsIntHashSet(int year, int day)
    {
        var input = ReadInput(year, day);

        HashSet<int> hashset = new(input.Length);
        for (int i = 0; i < input.Length; i++)
        {
            hashset.Add(int.Parse(input[i]));
        }

        return hashset;
    }

    public static int ReadInputAsInt(int year, int day)
    {
        return int.Parse(ReadInput(year, day)[0]);
    }
}
