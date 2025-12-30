using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe01 : IAufgabe
{
    private readonly string[] _input;
    private int _currentValue = 50;

    public Aufgabe01()
    {
        _input = Utilities.ReadInput(2025, 1);
    }

    public string Calc()
    {
        var count = 0;

        foreach (var line in _input)
        {
            var rotation = line[0];
            var value = int.Parse(line[1..]);

            switch (rotation)
            {
                case 'R':
                    _currentValue += value;
                    break;
                case 'L':
                    _currentValue -= value;
                    break;
            }

            if (_currentValue % 100 == 0)
            {
                count++;
            }
        }

        return count.ToString();
    }
}
