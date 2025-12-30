using System.Diagnostics.CodeAnalysis;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Utils;

internal readonly struct Point : IComparable<Point>
{
    public int X { get; private init; }
    public int Y { get; private init; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(string input)
    {
        var split = input.Split(',');
        X = int.Parse(split[0]);
        Y = int.Parse(split[1]);
    }

    public Point(int[] input)
    {
        X = input[0];
        Y = input[1];
    }

    public Point[] GetNeighbours()
    {
        return
        [
            new(X + 1, Y),
            new(X - 1, Y),
            new(X, Y + 1),
            new(X, Y - 1)
        ];
    }

    public Point[] GetFullNeighbours()
    {
        return
        [
            new(X + 1, Y),
            new(X - 1, Y),
            new(X, Y + 1),
            new(X, Y - 1),
            new(X + 1, Y + 1),
            new(X + 1, Y - 1),
            new(X - 1, Y + 1),
            new(X - 1, Y - 1)
        ];
    }

    public bool IsNeighbour(Point other)
    {
        return int.Abs(X - other.X) + int.Abs(Y - other.Y) == 1;
    }

    public bool IsFullNeighbour(Point other)
    {
        return int.Abs(X - other.X) <= 1 && int.Abs(Y - other.Y) <= 1;
    }

    public Point Move(Direction direction)
    {
        return Move(direction, 1);
    }

    public Point Move(Direction direction, int amount)
    {
        return direction switch
        {
            Direction.Right => new(X + amount, Y),
            Direction.Down => new(X, Y + amount),
            Direction.Left => new(X - amount, Y),
            Direction.Up => new(X, Y - amount),
            _ => throw new NotImplementedException(),
        };
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Point other)
        {
            return X == other.X && Y == other.Y;
        }

        return false;
    }

    public override string ToString()
    {
        return $"X={X},Y={Y}";
    }

    public string ToShortString()
    {
        return $"{X},{Y}";
    }

    public int GetManhattenDistanceToZero()
    {
        return int.Abs(X) + int.Abs(Y);
    }

    public int GetManhattenDistance(Point other)
    {
        return int.Abs(int.Max(X, other.X) - int.Min(X, other.X)) +
               int.Abs(int.Max(Y, other.Y) - int.Min(Y, other.Y));
    }

    public int CompareTo(Point other)
    {
        var compare = Y.CompareTo(other.Y);
        if (compare != 0)
        {
            return compare;
        }

        return X.CompareTo(other.X);
    }

    public static bool operator ==(Point left, Point right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Point left, Point right)
    {
        return !left.Equals(right);
    }
}