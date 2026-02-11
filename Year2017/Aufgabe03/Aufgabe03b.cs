using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe03b : IAufgabe
{
    private readonly uint _number;
    private readonly uint[,] _numbers;
    private char _direction = '^';

    private uint _currentX = GridSize;
    private uint _currentY = GridSize;
    private uint _cornerX = GridSize + 1;
    private uint _cornerY = GridSize - 1;
    private uint _lineLength = 1;

    private const uint GridSize = 9;

    public Aufgabe03b()
    {
        _number = Utilities.ReadInputAsT<uint>(2017, 3);

        _numbers = new uint[GridSize * 2 + 1, GridSize * 2 + 1];
    }

    public string Calc()
    {
        _numbers[_currentY, _currentX] = 1;
        _currentX++;

        uint result;
        do
        {
            result = GetResult(_currentX, _currentY);
            _numbers[_currentY, _currentX] = result;
            GetNextStep();
        } while (result <= _number);

        return result.ToString();
    }

    private uint GetResult(uint x, uint y)
    {
        return _numbers[y + 1, x + 1] +
               _numbers[y + 1, x] +
               _numbers[y + 1, x - 1] +
               _numbers[y, x + 1] +
               _numbers[y, x - 1] +
               _numbers[y - 1, x + 1] +
               _numbers[y - 1, x] +
               _numbers[y - 1, x - 1];
    }

    private void GetNextStep()
    {
        if (_currentX != _cornerX || _currentY != _cornerY)
        {
            switch (_direction)
            {
                case '^':
                    _currentY--;
                    break;
                case '>':
                    _currentX++;
                    break;
                case 'v':
                    _currentY++;
                    break;
                case '<':
                    _currentX--;
                    break;
            }

            return;
        }

        switch (_direction)
        {
            case '^':
                _direction = '<';
                _currentX--;
                _lineLength++;
                _cornerX -= _lineLength;
                break;
            case '>':
                _direction = '^';
                _currentY--;
                _cornerY -= _lineLength;
                break;
            case 'v':
                _direction = '>';
                _currentX++;
                _lineLength++;
                _cornerX += _lineLength;
                break;
            case '<':
                _direction = 'v';
                _currentY++;
                _cornerY += _lineLength;
                break;
        }
    }
}
