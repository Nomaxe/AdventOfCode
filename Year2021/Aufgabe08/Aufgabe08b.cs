using AdventOfCode.Utils;

namespace AdventOfCode.Year2021;

internal class Aufgabe08b : IAufgabe
{
    private readonly string[] _input;

    public Aufgabe08b()
    {
        _input = Utilities.ReadInput(2021, 8);
    }

    public string Calc()
    {
        int count = 0;

        foreach (var line in _input)
        {
            var input = line[..58].Split(' ');
            var output = line[61..].Split(' ');
            var complete = new string[input.Length + output.Length];
            input.CopyTo(complete, 0);
            output.CopyTo(complete, input.Length);

            DictionaryHashSet<char, char> list = new(8);
            list.AddRange('a', ['a', 'b', 'c', 'd', 'e', 'f', 'g']);
            list.AddRange('b', ['a', 'b', 'c', 'd', 'e', 'f', 'g']);
            list.AddRange('c', ['a', 'b', 'c', 'd', 'e', 'f', 'g']);
            list.AddRange('d', ['a', 'b', 'c', 'd', 'e', 'f', 'g']);
            list.AddRange('e', ['a', 'b', 'c', 'd', 'e', 'f', 'g']);
            list.AddRange('f', ['a', 'b', 'c', 'd', 'e', 'f', 'g']);
            list.AddRange('g', ['a', 'b', 'c', 'd', 'e', 'f', 'g']);

            foreach (var item in input)
            {
                switch (item.Length)
                {
                    case 2:
                        foreach (var character in item)
                        {
                            list.Remove(character, 'a');
                            list.Remove(character, 'b');
                            list.Remove(character, 'd');
                            list.Remove(character, 'e');
                            list.Remove(character, 'g');
                        }
                        break;
                    case 3:
                        foreach (var character in item)
                        {
                            list.Remove(character, 'b');
                            list.Remove(character, 'd');
                            list.Remove(character, 'e');
                            list.Remove(character, 'g');
                        }
                        break;
                    case 4:
                        foreach (var character in item)
                        {
                            list.Remove(character, 'a');
                            list.Remove(character, 'e');
                            list.Remove(character, 'g');
                        }
                        break;
                }
            }

            foreach (var a in list['a'])
            {
                foreach (var b in list['b'].Where(x => x != a))
                {
                    foreach (var c in list['c'].Where(x => x != a && x != b))
                    {
                        foreach (var d in list['d'].Where(x => x != a && x != b && c != x))
                        {
                            foreach (var e in list['e'].Where(x => x != a && x != b && c != x && d != x))
                            {
                                foreach (var f in list['f'].Where(x => x != a && x != b && c != x && d != x && e != x))
                                {
                                    foreach (var g in list['g'].Where(x => x != a && x != b && c != x && d != x && e != x && f != x))
                                    {
                                        if (Test(complete, a, b, c, d, e, f, g))
                                        {
                                            count += GetOutputValue(output, a, b, c, d, e, f, g);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        return count.ToString();
    }

    private static bool Test(string[] input, char a, char b, char c, char d, char e, char f, char g)
    {
        foreach (var item in input)
        {
            var value = GetValue(item, a, b, c, d, e, f, g);
            if (!value.HasValue)
            {
                return false;
            }
        }


        return true;
    }

    private static int GetOutputValue(string[] input, char a, char b, char c, char d, char e, char f, char g)
    {
        int value = 0;

        foreach (var item in input)
        {
            value *= 10;
            value += GetValue(item, a, b, c, d, e, f, g)!.Value;
        }

        return value;
    }

    private static int? GetValue(string input, char a, char b, char c, char d, char e, char f, char g)
    {
        bool[] light = [false, false, false, false, false, false, false];

        foreach (var character in input)
        {
            var index = character switch
            {
                'a' => a,
                'b' => b,
                'c' => c,
                'd' => d,
                'e' => e,
                'f' => f,
                'g' => g,
                _ => throw new NotImplementedException(),
            } - 'a';

            light[index] = true;
        }

        if (light[0] && light[1] && light[2] && !light[3] && light[4] && light[5] && light[6])
        {
            return 0;
        }
        else if (!light[0] && !light[1] && light[2] && !light[3] && !light[4] && light[5] && !light[6])
        {
            return 1;
        }
        else if (light[0] && !light[1] && light[2] && light[3] && light[4] && !light[5] && light[6])
        {
            return 2;
        }
        else if (light[0] && !light[1] && light[2] && light[3] && !light[4] && light[5] && light[6])
        {
            return 3;
        }
        else if (!light[0] && light[1] && light[2] && light[3] && !light[4] && light[5] && !light[6])
        {
            return 4;
        }
        else if (light[0] && light[1] && !light[2] && light[3] && !light[4] && light[5] && light[6])
        {
            return 5;
        }
        else if (light[0] && light[1] && !light[2] && light[3] && light[4] && light[5] && light[6])
        {
            return 6;
        }
        else if (light[0] && !light[1] && light[2] && !light[3] && !light[4] && light[5] && !light[6])
        {
            return 7;
        }
        else if (light[0] && light[1] && light[2] && light[3] && light[4] && light[5] && light[6])
        {
            return 8;
        }
        else if (light[0] && light[1] && light[2] && light[3] && !light[4] && light[5] && light[6])
        {
            return 9;
        }
        else
        {
            return null;
        }
    }
}
