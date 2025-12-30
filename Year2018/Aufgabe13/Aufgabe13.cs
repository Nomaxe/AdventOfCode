using AdventOfCode.Utils;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Year2018;

internal class Aufgabe13 : IAufgabe
{
    private readonly Grid _grid;
    private HashSet<Cart> _carts;

    public Aufgabe13()
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

        while (true)
        {
            HashSet<Cart> nextCarts = new(_carts.Count);

            foreach (var cart in _carts)
            {
                var nextCart = cart.Move(_grid);
                if (!nextCarts.Add(nextCart))
                {
                    return nextCart.Point.ToShortString();
                }
            }

            _carts = nextCarts;
        }
    }

    private struct Cart : IEquatable<Cart>
    {
        public Point Point { get; private init; }
        public Direction Direction { get; private set; }
        public Intersection Intersection { get; private set; }

        public Cart(Point point, Direction direction) : this(point, direction, Intersection.Left)
        {

        }

        private Cart(Point point, Direction direction, Intersection intersection)
        {
            Point = point;
            Direction = direction;
            Intersection = intersection;
        }

        public readonly Cart Move(Grid grid)
        {
            Cart nextCart = new(Point.Move(Direction), Direction, Intersection);
            var character = grid.GetValue(nextCart.Point);
            switch (character)
            {
                case '/':
                    nextCart.Direction = Direction switch
                    {
                        Direction.Right => Direction.Up,
                        Direction.Down => Direction.Left,
                        Direction.Left => Direction.Down,
                        Direction.Up => Direction.Right,
                        _ => throw new NotImplementedException(),
                    };
                    break;
                case '\\':
                    nextCart.Direction = Direction switch
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
                            nextCart.Direction = Direction.TurnLeft();
                            break;
                        case Intersection.Right:
                            nextCart.Direction = Direction.TurnRight();
                            break;
                    }

                    nextCart.Intersection = (Intersection)(((int)Intersection + 1) % 3);
                    break;
            }
            ;

            return nextCart;
        }

        public readonly override int GetHashCode()
        {
            return Point.GetHashCode();
        }

        public readonly bool Equals(Cart other)
        {
            return Point == other.Point;
        }

        public readonly override bool Equals(object? obj)
        {
            return obj is Cart other && Equals(other);
        }
    }

    private enum Intersection
    {
        Left,
        Straight,
        Right
    }
}
