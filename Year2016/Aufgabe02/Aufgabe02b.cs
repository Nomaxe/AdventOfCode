using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe02b : IAufgabe
{
    private readonly string[] _input;
    private readonly Grid<char> _grid;
    private int _positionX = 0;
    private int _positionY = 2;

    public Aufgabe02b()
    {
        _input = Utilities.ReadInput(2016, 2);
        _grid = new(5);

        _grid.SetValue(2, 0, '1');
        _grid.SetValue(1, 1, '2');
        _grid.SetValue(2, 1, '3');
        _grid.SetValue(3, 1, '4');
        _grid.SetValue(0, 2, '5');
        _grid.SetValue(1, 2, '6');
        _grid.SetValue(2, 2, '7');
        _grid.SetValue(3, 2, '8');
        _grid.SetValue(4, 2, '9');
        _grid.SetValue(1, 3, 'A');
        _grid.SetValue(2, 3, 'B');
        _grid.SetValue(3, 3, 'C');
        _grid.SetValue(2, 4, 'D');
    }

    public string Calc()
    {
        string result = string.Empty;

        foreach (var line in _input)
        {
            foreach (var character in line)
            {
                switch (character)
                {
                    case 'U':
                        if (_grid.GetValueOrDefault(_positionX, _positionY - 1) != '\0')
                        {
                            _positionY--;
                        }
                        break;
                    case 'R':
                        if (_grid.GetValueOrDefault(_positionX + 1, _positionY) != '\0')
                        {
                            _positionX++;
                        }
                        break;
                    case 'D':
                        if (_grid.GetValueOrDefault(_positionX, _positionY + 1) != '\0')
                        {
                            _positionY++;
                        }
                        break;
                    case 'L':
                        if (_grid.GetValueOrDefault(_positionX - 1, _positionY) != '\0')
                        {
                            _positionX--;
                        }
                        break;
                }
            }

            result += _grid.GetValue(_positionX, _positionY);
        }

        return result;
    }
}
