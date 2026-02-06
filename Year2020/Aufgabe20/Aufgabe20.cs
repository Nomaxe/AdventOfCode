using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2020;

internal class Aufgabe20 : IAufgabe 
{
    private readonly string[] _input;
    private readonly DictionaryList<int, string> _tiles;
    private readonly DictionaryCounter<string> _counter;

    public Aufgabe20()
    {
        _input = Utilities.ReadInput(2020, 20);
        _tiles = new();
        _counter = new();
    }

    public string Calc()
    {
        for (int i = 0; i < _input.Length; i += 12)
        {
            AddTile(i);
        }

        long result = 1;

        foreach (var tile in _tiles)
        {
            int count = 0;

            foreach (var item in tile.Value)
            {
                count += _counter[item];
            }

            if (count == 6)
            {
                result *= tile.Key;
            }
        }

        return result.ToString();
    }

    private void AddTile(int index)
    {
        var id = _input[index].GetNumber(5);

        var top = _input[index + 1];
        var bottom = _input[index + 10];
        (var left, var right) = GetVerticalLines(index + 1);

        _tiles.Add(id, [top, bottom, left, right]);
        _counter.Add(top);
        _counter.Add(top.Reverse());
        _counter.Add(bottom);
        _counter.Add(bottom.Reverse());
        _counter.Add(left);
        _counter.Add(left.Reverse());
        _counter.Add(right);
        _counter.Add(right.Reverse());
    }

    private (string, string) GetVerticalLines(int index)
    {
        StringBuilder left = new();
        StringBuilder right = new();

        for (int i = 0; i < 10; i++)
        {
            left.Append(_input[index + i][0]);
            right.Append(_input[index + i][9]);
        }

        return (left.ToString(), right.ToString());
    }
}
