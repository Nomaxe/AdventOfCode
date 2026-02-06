using AdventOfCode.Utils;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2020;

internal class Aufgabe20b
{
    private readonly string[] _input;
    private readonly List<Tile> _tiles;
    private Grid _grid;
    private string _topInColumn0Connection;
    private string _rightInRowConnection;
    private const int TileCount = 12;

    public Aufgabe20b()
    {
        _input = Utilities.ReadInput(2020, 20);
        _tiles = new(TileCount * TileCount);
        _grid = new(TileCount * 8);
        _topInColumn0Connection = string.Empty;
        _rightInRowConnection = string.Empty;
    }

    public string Calc()
    {
        DictionaryCounter<string> counter = new();

        for (int i = 0; i < _input.Length; i += 12)
        {
            Tile tile = new(_input[(i + 1)..(i + 11)]);
            _tiles.Add(tile);
            counter.Add(tile.Top);
            counter.Add(tile.TopReverse);
            counter.Add(tile.Left);
            counter.Add(tile.LeftReverse);
            counter.Add(tile.Right);
            counter.Add(tile.RightReverse);
            counter.Add(tile.Bottom);
            counter.Add(tile.BottomReverse);
        }

        for (int y = 0; y < TileCount; y++)
        {
            if (y == 0)
            {
                var grid = GetFirstGrid(counter);
                _grid.CopyValuesOfGrid(grid, 0, 0, 1, 8);
            }
            else
            {
                var grid = GetNextGridForColumn();
                _grid.CopyValuesOfGrid(grid, 0, y * 8, 1, 8);
            }

            for (int x = 1; x < TileCount; x++)
            {
                var grid = GetNextGridForRow();
                _grid.CopyValuesOfGrid(grid, x * 8, y * 8, 1, 8);
            }
        }

        return GetCount().ToString();
    }

    private Grid GetFirstGrid(DictionaryCounter<string> counter)
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            var tile = _tiles[i];

            var countTop = counter[tile.Top];
            var countBottom = counter[tile.Bottom];
            var countLeft = counter[tile.Left];
            var countRight = counter[tile.Right];

            if (countTop + countRight == 2)
            {
                _tiles.RemoveAt(i);
                _topInColumn0Connection = tile.BottomReverse;
                _rightInRowConnection = tile.Left;
                return tile.Grid.FlipVertical();
            }
            else if (countRight + countBottom == 2)
            {
                _tiles.RemoveAt(i);
                _topInColumn0Connection = tile.TopReverse;
                _rightInRowConnection = tile.LeftReverse;
                return tile.Grid.Rotate180();
            }
            else if (countBottom + countLeft == 2)
            {
                _tiles.RemoveAt(i);
                _topInColumn0Connection = tile.Top;
                _rightInRowConnection = tile.RightReverse;
                return tile.Grid.FlipHorizontal();
            }
            else if (countLeft + countTop == 2)
            {
                _tiles.RemoveAt(i);
                _topInColumn0Connection = tile.Bottom;
                _rightInRowConnection = tile.Right;
                return tile.Grid;
            }
        }

        throw new NotImplementedException();
    }

    private Grid GetNextGridForRow()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            var tile = _tiles[i];

            if (_rightInRowConnection == tile.Top)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Bottom;
                return tile.Grid.RotateLeft().FlipHorizontal();
            }
            if (_rightInRowConnection == tile.TopReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.BottomReverse;
                return tile.Grid.RotateLeft();
            }
            if (_rightInRowConnection == tile.Left)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Right;
                return tile.Grid;
            }
            if (_rightInRowConnection == tile.LeftReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.RightReverse;
                return tile.Grid.FlipHorizontal();
            }
            if (_rightInRowConnection == tile.Right)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Left;
                return tile.Grid.FlipVertical();
            }
            if (_rightInRowConnection == tile.RightReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.LeftReverse;
                return tile.Grid.Rotate180();
            }
            if (_rightInRowConnection == tile.Bottom)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Top;
                return tile.Grid.RotateRight();
            }
            if (_rightInRowConnection == tile.BottomReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.TopReverse;
                return tile.Grid.RotateRight().FlipHorizontal();
            }
        }

        throw new NotImplementedException();
    }

    private Grid GetNextGridForColumn()
    {
        for (int i = 0; i < _tiles.Count; i++)
        {
            var tile = _tiles[i];

            if (_topInColumn0Connection == tile.Top)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Right;
                _topInColumn0Connection = tile.Bottom;
                return tile.Grid;
            }
            if (_topInColumn0Connection == tile.TopReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Left;
                _topInColumn0Connection = tile.BottomReverse;
                return tile.Grid.FlipVertical();
            }
            if (_topInColumn0Connection == tile.Left)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Bottom;
                _topInColumn0Connection = tile.Right;
                return tile.Grid.RotateRight().FlipVertical();
            }
            if (_topInColumn0Connection == tile.LeftReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.Top;
                _topInColumn0Connection = tile.RightReverse;
                return tile.Grid.RotateRight();
            }
            if (_topInColumn0Connection == tile.Right)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.BottomReverse;
                _topInColumn0Connection = tile.Left;
                return tile.Grid.RotateLeft();
            }
            if (_topInColumn0Connection == tile.RightReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.TopReverse;
                _topInColumn0Connection = tile.LeftReverse;
                return tile.Grid.RotateLeft().FlipVertical();
            }
            if (_topInColumn0Connection == tile.Bottom)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.RightReverse;
                _topInColumn0Connection = tile.Top;
                return tile.Grid.FlipHorizontal();
            }
            if (_topInColumn0Connection == tile.BottomReverse)
            {
                _tiles.RemoveAt(i);
                _rightInRowConnection = tile.LeftReverse;
                _topInColumn0Connection = tile.TopReverse;
                return tile.Grid.Rotate180();
            }
        }

        throw new NotImplementedException();
    }

    private int GetCount()
    {
        int count = 0;
        int loopCount = 0;

        do
        {
            for (int y = 1; y < _grid.SizeY - 1; y++)
            {
                for (int x = 0; x < _grid.SizeX - 18; x++)
                {
                    if (_grid.GetValue(x, y) == '#' && _grid.GetValue(x + 5, y) == '#' && _grid.GetValue(x + 6, y) == '#' && _grid.GetValue(x + 11, y) == '#' && 
                        _grid.GetValue(x + 12, y) == '#' && _grid.GetValue(x + 17, y) == '#' && _grid.GetValue(x + 18, y) == '#' && _grid.GetValue(x + 19, y) == '#' &&
                        _grid.GetValue(x + 18, y - 1) == '#' && _grid.GetValue(x + 1, y + 1) == '#' && _grid.GetValue(x + 4, y + 1) == '#' && _grid.GetValue(x + 7, y + 1) == '#' &&
                        _grid.GetValue(x + 10, y + 1) == '#' && _grid.GetValue(x + 13, y + 1) == '#' && _grid.GetValue(x + 16, y + 1) == '#')
                    {
                        count++;
                    }
                }
            }

            if (count > 0)
            {
                return _grid.GetCountOfValue('#') - count * 15;
            }

            loopCount++;
            _grid = _grid.RotateRight();

            if (loopCount == 4)
            {
                _grid = _grid.FlipVertical();
            }
        } while (true);
    }

    private class Tile
    {
        public string Top { get; private init; }
        public string TopReverse { get; private init; }
        public string Left { get; private init; }
        public string LeftReverse { get; private init; }
        public string Right { get; private init; }
        public string RightReverse { get; private init; }
        public string Bottom { get; private init; }
        public string BottomReverse { get; private init; }
        public Grid Grid { get; private init; }

        public Tile(string[] input)
        {
            Grid = Grid.CreateCharGrid(input);
            Top = input[0];
            Bottom = input[9];

            StringBuilder left = new(10);
            StringBuilder right = new(10);

            for (int i = 0; i < input.Length; i++)
            {
                left.Append(input[i][0]);
                right.Append(input[i][9]);
            }

            Left = left.ToString();
            Right = right.ToString();

            TopReverse = Top.Reverse();
            LeftReverse = Left.Reverse();
            RightReverse = Right.Reverse();
            BottomReverse = Bottom.Reverse();
        }
    }
}
