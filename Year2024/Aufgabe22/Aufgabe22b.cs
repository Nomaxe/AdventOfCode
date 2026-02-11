using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe22b : IAufgabe
{
    private readonly string[] _input;
    private readonly List<ulong> _secrets;
    private readonly LargeCounter<(int a, int b, int c, int d)> _counter = [];

    public Aufgabe22b()
    {
        _input = Utilities.ReadInput(2024, 22);
        _secrets = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            _secrets.Add(ulong.Parse(line));
        }

        foreach (var secret in _secrets)
        {
            List<Bananas> list = [];
            HashSet<(int a, int b, int c, int d)> alreadyAdded = [];
            ulong current = secret;

            for (int i = 0; i < 2000; i++)
            {
                var former = current;
                current = CalcSecret(current);
                list.Add(new(current % 10, current % 10 - former % 10));

                if (list.Count == 4)
                {
                    if (!alreadyAdded.Contains((list[0].Change, list[1].Change, list[2].Change, list[3].Change)))
                    {
                        _counter.Add((list[0].Change, list[1].Change, list[2].Change, list[3].Change), list[3].Number);
                        alreadyAdded.Add((list[0].Change, list[1].Change, list[2].Change, list[3].Change));
                    }
                    list.RemoveAt(0);
                }
            }
        }

        return _counter.Max().ToString();
    }

    private static ulong CalcSecret(ulong secret)
    {
        secret = MixAndPrune(secret, secret * 64);
        secret = MixAndPrune(secret, secret / 32);
        secret = MixAndPrune(secret, secret * 2048);
        return secret;
    }

    private static ulong MixAndPrune(ulong current, ulong value)
    {
        var secret = current ^ value;
        secret %= 16777216;
        return secret;
    }

    private readonly struct Bananas
    {
        public byte Number { get; private init; }
        public short Change { get; private init; }

        public Bananas(ulong number, ulong change)
        {
            Number = (byte)number;
            Change = (short)change;
        }

        public override readonly string ToString()
        {
            return $"Number={Number},Change={Change}";
        }
    }
}
