using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe22 : IAufgabe
{
    private readonly string[] _input;
    private readonly Queue<int> _player1;
    private readonly Queue<int> _player2;

    public Aufgabe22()
    {
        _input = Utilities.ReadInput(2020, 22);
        _player1 = new(_input.Length - 3);
        _player2 = new(_input.Length - 3);
    }

    public string Calc()
    {
        var currentList = _player1;

        for (int i = 1; i < _input.Length; i++)
        {
            if (string.IsNullOrEmpty(_input[i]))
            {
                currentList = _player2;
                i++;
                continue;
            }

            currentList.Enqueue(int.Parse(_input[i]));
        }

        do
        {
            var player1Card = _player1.Dequeue();
            var player2Card = _player2.Dequeue();

            if (player1Card > player2Card)
            {
                _player1.Enqueue(player1Card);
                _player1.Enqueue(player2Card);
            }
            else
            {
                _player2.Enqueue(player2Card);
                _player2.Enqueue(player1Card);
            }
        } while (_player1.Count > 0 && _player2.Count > 0);

        currentList = _player1.Count > 0 ? _player1 : _player2;
        int result = 0;

        for (int i = currentList.Count; i >= 1; i--)
        {
            result += currentList.Dequeue() * i;
        }

        return result.ToString();
    }
}
