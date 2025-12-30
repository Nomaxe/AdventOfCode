using AdventOfCode.Utils;
using System.Runtime.InteropServices;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2018;

internal class Aufgabe13b : IAufgabe
{
    private readonly Grid _grid;
    private readonly List<Cart> _carts;

    public Aufgabe13b()
    {
        _grid = Grid.CreateCharGrid(2018, 13);
        _carts = new();
    }

    public string Calc()
    {
        foreach (var point in _grid.GetPointsOfValue('<'))
        {
            _carts.Add(new(point, Direction.Left));
            _grid.SetValue(point, '-');
        }
        foreach (var point in _grid.GetPointsOfValue('^'))
        {
            _carts.Add(new(point, Direction.Up));
            _grid.SetValue(point, '|');
        }
        foreach (var point in _grid.GetPointsOfValue('>'))
        {
            _carts.Add(new(point, Direction.Right));
            _grid.SetValue(point, '-');
        }
        foreach (var point in _grid.GetPointsOfValue('v'))
        {
            _carts.Add(new(point, Direction.Down));
            _grid.SetValue(point, '|');
        }

        List<int> toRemove = new();
        while (true)
        {
            toRemove.Clear();
            _carts.Sort();

            var span = CollectionsMarshal.AsSpan(_carts);
            for (int i = 0; i < span.Length; i++)
            {
                ref var cart = ref span[i];
                cart.Move(_grid);

                for (int j = 0; j < _carts.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (cart.Point == _carts[j].Point)
                    {
                        toRemove.Add(i);
                        toRemove.Add(j);
                    }
                }
            }

            foreach (var toRemoveItem in toRemove.OrderDescending())
            {
                _carts.RemoveAt(toRemoveItem);
            }

            if (_carts.Count == 1)
            {
                return _carts[0].Point.ToShortString();
            }
        }
    }

    private struct Cart : IComparable<Cart>
    {
        public Point Point { get; private set; }
        public Direction Direction { get; private set; }
        public Intersection Intersection { get; private set; }

        public Cart(Point point, Direction direction)
        {
            Point = point;
            Direction = direction;
            Intersection = Intersection.Left;
        }

        public void Move(Grid grid)
        {
            Point = Point.Move(Direction);
            var character = grid.GetValue(Point);
            switch (character)
            {
                case '/':
                    Direction = Direction switch
                    {
                        Direction.Right => Direction.Up,
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Down,
                        Direction.Up => Direction.Right,
                        _ => throw new NotImplementedException(),
                    };
                    break;
                case '\\':
                    Direction = Direction switch
                    {
                        Direction.Right => Direction.Down,
                        Direction.Down => Direction.Right,
                        Direction.Left => Direction.Up,
                        Direction.Up => Direction.Left,
                        _ => throw new NotImplementedException(),
                    };
                    break;
                case '+':
                    switch (Intersection)
                    {
                        case Intersection.Left:
                            Direction = Direction.TurnLeft();
                            break;
                        case Intersection.Right:
                            Direction = Direction.TurnRight();
                            break;
                    }

                    Intersection = (Intersection)(((int)Intersection + 1) % 3);
                    break;
            }
        }

        public readonly int CompareTo(Cart other)
        {
            var compare = Point.Y.CompareTo(other.Point.Y);
            if (compare != 0)
            {
                return compare;
            }

            return Point.X.CompareTo(other.Point.X);
        }
    }

    private enum Intersection
    {
        Left,
        Straight,
        Right
    }
}
