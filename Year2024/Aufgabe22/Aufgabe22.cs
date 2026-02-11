using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe22 : IAufgabe
{
    private readonly string[] _input;
    private readonly List<ulong> _secrets;

    public Aufgabe22()
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

        ulong result = 0;

        foreach (var secret in _secrets)
        {
            ulong current = secret;

            for (int i = 0; i < 2000; i++)
            {
                current = CalcSecret(current);
            }

            result += current;
        }

        return result.ToString();
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
}
