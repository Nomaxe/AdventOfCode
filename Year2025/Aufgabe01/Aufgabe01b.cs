using AdventOfCode.Utils;

namespace AdventOfCode.Year2025;

internal class Aufgabe01b : IAufgabe
{
    private readonly string[] _input;
    private int _currentValue = 50;
    private int _count = 0;

    public Aufgabe01b()
    {
        _input = Utilities.ReadInput(2025, 1);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var rotation = line[0];
            var value = int.Parse(line[1..]);

            switch (rotation)
            {
                case 'R':
                    //2 -> 7
                    //92 -> 113             +1
                    //92 -> 213             +2
                    //92 -> 100             +1
                    //0 -> 92
                    //0 -> 100              +1
                    _currentValue += value;

                    while (_currentValue >= 100)
                    {
                        _currentValue -= 100;
                        _count++;
                    }
                    break;
                case 'L':
                    //7 -> 2
                    //13 -> -18             +1
                    //13 -> -118            +2
                    //13 -> 0               +1
                    //0 -> -13
                    //0 -> -100             +1
                    //30 -> 0               +1
                    if (_currentValue == 0)
                    {
                        _count--;
                    }

                    _currentValue -= value;

                    while (_currentValue < 0)
                    {
                        _currentValue += 100;
                        _count++;
                    }

                    if (_currentValue == 0)
                    {
                        _count++;
                    }
                    break;
            }
        }
        return _count.ToString();
    }
}
