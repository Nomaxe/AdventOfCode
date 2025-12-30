using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe22 : IAufgabe
{
    private readonly Grid<Node> _grid;

    public Aufgabe22()
    {
        var input = Utilities.ReadInput(2016, 22);
        var sizeX = input[^1].GetNumber(16) + 1;
        _grid = new(sizeX, (input.Length - 2) / sizeX);

        foreach (var line in input.Skip(2))
        {
            var x = line.GetNumber(16);
            var y = line.GetNumber(x >= 10 ? 20 : 19);

            _grid.SetValue(x, y, new(line));
        }
    }

    public string Calc()
    {
        int count = 0;

        for (int y = 0; y < _grid.SizeY; y++)
        {
            for (int x = 0; x < _grid.SizeX; x++)
            {
                var size = _grid.GetValue(x, y).Used;

                if (size == 0)
                {
                    continue;
                }

                for (int y2 = 0; y2 < _grid.SizeY; y2++)
                {
                    for (int x2 = 0; x2 < _grid.SizeX; x2++)
                    {
                        if (x == x2 && y == y2)
                        {
                            continue;
                        }

                        if (_grid.GetValue(x2, y2).Avail >= size)
                        {
                            count++;
                        }
                    }
                }
            }
        }

        return count.ToString();
    }

    private readonly struct Node
    {
        public int Size { get; private init; }
        public int Used { get; private init; }
        public int Avail => Size - Used;

        public Node(string input)
        {
            Size = input.GetNumberWhitespace(24);
            Used = input.GetNumberWhitespace(30);
        }
    }
}
