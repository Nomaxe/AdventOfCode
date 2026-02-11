using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe09 : IAufgabe
{
    private readonly List<int> _marbles;
    private readonly int _marblesCount;
    private int _currentMarble = 0;

    private readonly int _playerCount;
    private int _player = 0;

    private readonly LargeCounter<int> _players;

    public Aufgabe09()
    {
        var numbers = Utilities.ReadInputAsString(2018, 9).GetUnsignedNumbers();
        _playerCount = numbers[0];
        _marblesCount = numbers[1];
        _marbles = new(_marblesCount);
        _players = new(_playerCount);
    }

    public string Calc()
    {
        _marbles.Add(0);
        for (int i = 1; i <= _marblesCount; i++)
        {
            _player = (_player + 1) % _playerCount;

            if (i % 23 != 0)
            {
                AddMarble(i);
                GetNextPosition(2);
            }
            else
            {
                _currentMarble -= 8;
                if (_currentMarble < 0)
                {
                    _currentMarble += _marbles.Count;
                }

                _players.Add(_player, (ulong)i);
                _players.Add(_player, (ulong)_marbles[_currentMarble]);
                _marbles.RemoveAt(_currentMarble);
                GetNextPosition(1);
            }
        }

        return _players.Max().ToString();
    }

    private void AddMarble(int value)
    {
        if (_currentMarble == _marbles.Count)
        {
            _marbles.Add(value);
        }
        else
        {
            _marbles.Insert(_currentMarble + 1, value);
        }
    }

    private void GetNextPosition(int value)
    {
        _currentMarble += value;
        _currentMarble %= _marbles.Count;
    }
}
