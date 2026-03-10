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

    public readonly Point[] GetNeighbours()
    {
        return
        [
            new(X + 1, Y),
            new(X - 1, Y),
            new(X, Y + 1),
            new(X, Y - 1)
        ];
    }

    public readonly Point[] GetFullNeighbours()
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

    public readonly bool IsNeighbour(Point other)
    {
        return int.Abs(X - other.X) + int.Abs(Y - other.Y) == 1;
    }

    public readonly bool IsFullNeighbour(Point other)
    {
        return int.Abs(X - other.X) <= 1 && int.Abs(Y - other.Y) <= 1;
    }

    public readonly Point Move(int horizontal, int vertical)
    {
        return new Point(X + horizontal, Y + vertical);
    }

    public readonly Point Move(Direction direction)
    {
        return Move(direction, 1);
    }

    public readonly Point Move(Direction direction, int amount)
    {
        return direction switch
        {
            Direction.Right => MoveRight(amount),
            Direction.Down => MoveDown(amount),
            Direction.Left => MoveLeft(amount),
            Direction.Up => MoveUp(amount),
            _ => throw new NotImplementedException(),
        };
    }

    public readonly Point MoveRight(int amount)
    {
        return new(X + amount, Y);
    }

    public readonly Point MoveDown(int amount)
    {
        return new(X, Y + amount);
    }

    public readonly Point MoveLeft(int amount)
    {
        return new(X - amount, Y);
    }

    public readonly Point MoveUp(int amount)
    {
        return new(X, Y - amount);
    }

    public readonly Point RotateRight()
    {
        return new(-Y, X);
    }

    public readonly Point RotateLeft()
    {
        return new(Y, -X);
    }

    public readonly Point Rotate180()
    {
        return new(-X, -Y);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public readonly override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Point other)
        {
            return X == other.X && Y == other.Y;
        }

        return false;
    }

    public readonly override string ToString()
    {
        return $"X={X},Y={Y}";
    }

    public readonly string ToShortString()
    {
        return $"{X},{Y}";
    }

    public readonly int GetManhattenDistanceToZero()
    {
        return int.Abs(X) + int.Abs(Y);
    }

    public readonly int GetManhattenDistance(Point other)
    {
        return int.Abs(int.Max(X, other.X) - int.Min(X, other.X)) +
               int.Abs(int.Max(Y, other.Y) - int.Min(Y, other.Y));
    }

    public readonly int CompareTo(Point other)
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