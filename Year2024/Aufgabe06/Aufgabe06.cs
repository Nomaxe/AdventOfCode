using AdventOfCode.Utils;

namespace AdventOfCode.Year2024;

internal class Aufgabe06 : IAufgabe
{
    private readonly char[,] _input;
    private readonly char[,] _orginal;
    private readonly List<(int PosX, int PosY)> _visitedBlocks;
    private Direction _direction;
    private int _currentPosX;
    private int _currentPosY;

    public Aufgabe06()
    {
        var input = Utilities.ReadInput(2024, 6);
        _input = new char[input.Length, input[0].Length];
        _orginal = new char[input.Length, input[0].Length];
        _visitedBlocks = [];
        for (var x = 0; x < input[0].Length; x++)
        {
            for (var y = 0; y < input.Length; y++)
            {
                _input[y, x] = input[y][x];
            }
        }

        _currentPosY = Array.FindIndex(input, x => x.Contains('^'));
        _currentPosX = input[_currentPosY].IndexOf('^');
        _direction = Direction.Up;
        Array.Copy(_input, _orginal, _input.Length);
    }

    public string Calc()
    {
        int posX, posY;
        (posX, posY) = GetNextPosition();

        while (!IsOutOfBounds(posX, posY))
        {
            _input[_currentPosY, _currentPosX] = 'X';
            var nextElement = _input[posY, posX];
            switch (nextElement)
            {
                case '.':
                    _currentPosX = posX;
                    _currentPosY = posY;
                    _visitedBlocks.Add(new(_currentPosX, _currentPosY));
                    break;
                case 'X':
                    _currentPosX = posX;
                    _currentPosY = posY;
                    break;
                case '#':
                    SetNextDirection();
                    break;
                default:
                    throw new NotImplementedException();
            }

            (posX, posY) = GetNextPosition();
        }
        ;

        int result = 0;
        for (var x = 0; x < _input.GetLength(1); x++)
        {
            for (var y = 0; y < _input.GetLength(0); y++)
            {
                if (_input[y, x] == 'X')
                {
                    result++;
                }
            }
        }

        return (result + 1).ToString();
    }

    private (int PosX, int PosY) GetNextPosition()
    {
        return _direction switch
        {
            Direction.Up => (_currentPosX, _currentPosY - 1),
            Direction.Right => (_currentPosX + 1, _currentPosY),
            Direction.Down => (_currentPosX, _currentPosY + 1),
            Direction.Left => (_currentPosX - 1, _currentPosY),
            _ => throw new NotImplementedException(),
        };
    }

    private bool IsOutOfBounds(int posX, int posY)
    {
        return posY < 0 || posY >= _input.GetLength(0) || posX < 0 || posX >= _input.GetLength(1);
    }

    private void SetNextDirection()
    {
        _direction = _direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new NotImplementedException(),
        };
    }

    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
}
