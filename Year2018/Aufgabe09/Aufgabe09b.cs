using AdventOfCode.Utils;

namespace AdventOfCode.Year2018;

internal class Aufgabe09b : IAufgabe
{
    private readonly LinkedList<int> _marbles;
    private readonly int _marblesCount;
    private LinkedListNode<int> _currentMarble;

    private readonly int _playerCount;
    private int _player = 0;

    private readonly LargeCounter<int> _players;

    public Aufgabe09b()
    {
        var numbers = Utilities.ReadInput(2018, 9)[0].GetUnsignedNumbers();
        _playerCount = numbers[0];
        _marblesCount = numbers[1] * 100;
        _marbles = new();
        _currentMarble = _marbles.AddFirst(0);
        _players = new(_playerCount);
    }

    public string Calc()
    {
        for (int i = 1; i <= _marblesCount; i++)
        {
            _player = (_player + 1) % _playerCount;

            if (i % 23 != 0)
            {
                AddMarble(i);
                GetNextPosition();
            }
            else
            {
                GetPreviousPosition();
                GetPreviousPosition();
                GetPreviousPosition();
                GetPreviousPosition();
                GetPreviousPosition();
                GetPreviousPosition();
                GetPreviousPosition();
                GetPreviousPosition();

                _players.Add(_player, (ulong)i);
                _players.Add(_player, (ulong)_currentMarble.Value);
                var toRemove = _currentMarble;
                GetNextPosition();
                GetNextPosition();
                _marbles.Remove(toRemove);
            }
        }

        return _players.Max().ToString();
    }

    private void AddMarble(int value)
    {
        _currentMarble = _marbles.AddAfter(_currentMarble, value);
    }

    private void GetNextPosition()
    {
        _currentMarble = _currentMarble.Next ?? _marbles.First ?? throw new NullReferenceException();
    }

    private void GetPreviousPosition()
    {
        _currentMarble = _currentMarble.Previous ?? _marbles.Last ?? throw new NullReferenceException();
    }
}
