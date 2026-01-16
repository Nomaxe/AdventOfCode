using AdventOfCode.Utils;
using AdventOfCode.Year2018.Namespace16;

namespace AdventOfCode.Year2018;

internal class Aufgabe16b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryHashSet<int, string> _opcodes = new(16);

    public Aufgabe16b()
    {
        _input = Utilities.ReadInput(2018, 16);

        for (int i = 0; i < 16; i++)
        {
            _opcodes.AddRange(i, ["AddR", "AddI", "MulR", "MulI", "BAnR", "BAnI", "BOrR", "BOrI", "SetR", "SetI", "GTIR", "GTRI", "GTRR", "EqIR", "EqRI", "EqRR"]);
        }
    }

    public string Calc()
    {
        Device device = new();
        int index = 0;

        for (int i = 0; i < _input.Length; i += 4)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                _opcodes.RemoveDuplicatesUntilSingleItem();

                index = i;
                break;
            }

            var split1 = _input[i][9..^1].Split(',');
            var split2 = _input[i + 1].Split(' ');
            var split3 = _input[i + 2][9..^1].Split(',');
            var possibleOpcodes = device.GetPossibleOpcodes(int.Parse(split2[1]), int.Parse(split2[2]), int.Parse(split2[3]),
                                                            int.Parse(split1[0]), int.Parse(split1[1]), int.Parse(split1[2]), int.Parse(split1[3]),
                                                            int.Parse(split3[0]), int.Parse(split3[1]), int.Parse(split3[2]), int.Parse(split3[3]));

            var opcode = int.Parse(split2[0]);
            var list = _opcodes[opcode];
            foreach (var item in list)
            {
                if (!possibleOpcodes.Contains(item))
                {
                    list.Remove(item);
                }
            }
        }

        device = new();
        foreach (var line in _input.Skip(index + 2))
        {
            var split = line.Split(' ');
            device.AddInstruction(_opcodes[int.Parse(split[0])].First(), int.Parse(split[1]), int.Parse(split[2]), int.Parse(split[3]));
        }

        device.Calc();

        return device.RegisterA.ToString();
    }
}
