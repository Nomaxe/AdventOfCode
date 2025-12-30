using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe10b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<string, int> _bots;
    private readonly DictionaryList<string, string> _botInstructions;
    private readonly Dictionary<string, int> _output;

    public Aufgabe10b()
    {
        _input = Utilities.ReadInput(2016, 10);
        _bots = new(_input.Length);
        _botInstructions = new(_input.Length);
        _output = [];
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

                var instructions = _botInstructions[bot.Key];
                SendChip(low, instructions[0]);
                SendChip(high, instructions[1]);
                bot.Value.Clear();

                if (_output.TryGetValue("0", out int value0) && _output.TryGetValue("1", out int value1) && _output.TryGetValue("2", out int value2))
                {
                    return (value0 * value1 * value2).ToString();
                }
            }
        }
    }

    private void SendChip(int value, string output)
    {
        if (output[0] == 'b')
        {
            _bots.Add(output[4..], value);
        }
        else
        {
            _output.Add(output[7..], value);
        }
    }
}
