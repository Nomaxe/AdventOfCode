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

    public static List<T> ReadInputAsList<T>(int year, int day)
    {
        var input = ReadInput(year, day);

        List<T> list = new(input.Length);
        var type = typeof(T);

        foreach (var line in input)
        {
            list.Add((T)Convert.ChangeType(line, type));
        }

        return list;
    }

    public static List<T> ReadInputAsList<T>(int year, int day, char seperator)
    {
        var input = ReadInputAsString(year, day);
        return input.ToList<T>(seperator);
    }

    public static T[] ReadInputAsArray<T>(int year, int day)
    {
        var input = ReadInput(year, day);

        var array = new T[input.Length];
        var type = typeof(T);

        for (int i = 0; i < input.Length; i++)
        {
            array[i] = (T)Convert.ChangeType(input[i], type);
        }

        return array;
    }

    public static HashSet<T> ReadInputAsHashSet<T>(int year, int day)
    {
        var input = ReadInput(year, day);

        HashSet<T> hashset = new(input.Length);
        var type = typeof(T);

        for (int i = 0; i < input.Length; i++)
        {
            hashset.Add((T)Convert.ChangeType(input[i], type));
        }

        return hashset;
    }

    public static T ReadInputAsT<T>(int year, int day)
    {
        return (T)Convert.ChangeType(ReadInputAsString(year, day), typeof(T));
    }
}
