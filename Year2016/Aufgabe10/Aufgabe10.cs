using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe10 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, int> _bots;
    private readonly DictionaryList<string, string> _botInstructions;

    public Aufgabe10()
    {
        _input = Utilities.ReadInput(2016, 10);
        _bots = new(_input.Length);
        _botInstructions = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            if (line[0] == 'b')
            {
                var split = line.Split(' ');
                _botInstructions.Add(split[1], $"{split[5]} {split[6]}");
                _botInstructions.Add(split[1], $"{split[10]} {split[11]}");
            }
            else
            {
                var value = line[6..].GetNumber();
                var index = line.LastIndexOf(' ');
                _bots.Add(line[(index + 1)..], value);
            }
        }

        while (true)
        {
            var list = _bots.Where(x => x.Value.Count == 2).ToList();

            foreach (var bot in list)
            {
                var low = bot.Value.Min();
                var high = bot.Value.Max();

                if (low == 17 && high == 61)
                {
                    return bot.Key;
                }

                var instructions = _botInstructions[bot.Key];
                SendChip(low, instructions[0]);
                SendChip(high, instructions[1]);
                bot.Value.Clear();
            }
        }
    }

    private void SendChip(int value, string output)
    {
        if (output[0] == 'b')
        {
            _bots.Add(output[4..], value);
        }
    }
}
