using System.Diagnostics.CodeAnalysis;
using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Utils;

internal readonly struct PointHexGrid : IComparable<PointHexGrid>
{
    public int X { get; private init; }
    public int Y { get; private init; }

    public PointHexGrid(int x, int y)
    {
        X = x;
        Y = y;
    }

    public readonly PointHexGrid[] GetNeighbours()
    {
        return
        [
            new(X, Y - 1),
            new(X + 1, Y - 1),
            new(X + 1, Y),
            new(X, Y + 1),
            new(X - 1, Y + 1),
            new(X - 1, Y)
        ];
    }
    public readonly PointHexGrid Move(DirectionHex direction)
    {
        return direction switch
        {
            DirectionHex.UpRight => MoveUpRight(),
            DirectionHex.Right => MoveRight(),
            DirectionHex.DownRight => MoveDownRight(),
            DirectionHex.DownLeft => MoveDownLeft(),
            DirectionHex.Left => MoveLeft(),
            DirectionHex.UpLeft => MoveUpLeft(),
            _ => throw new NotImplementedException(),
        };
    }

    public readonly PointHexGrid MoveUpRight()
    {
        return new(X + 1, Y - 1);
    }

    public readonly PointHexGrid MoveRight()
    {
        return new(X + 1, Y);
    }

    public readonly PointHexGrid MoveDownRight()
    {
        return new(X, Y + 1);
    }

    public readonly PointHexGrid MoveDownLeft()
    {
        return new(X - 1, Y + 1);
    }

    public readonly PointHexGrid MoveLeft()
    {
        return new(X - 1, Y);
    }

    public readonly PointHexGrid MoveUpLeft()
    {
        return new(X, Y - 1);
    }

    public readonly override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public readonly override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is PointHexGrid other)
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

    public readonly int CompareTo(PointHexGrid other)
    {
        var compare = Y.CompareTo(other.Y);
        if (compare != 0)
        {
            return compare;
        }

        return X.CompareTo(other.X);
    }

    public static bool operator ==(PointHexGrid left, PointHexGrid right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(PointHexGrid left, PointHexGrid right)
    {
        return !left.Equals(right);
    }
}