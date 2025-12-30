using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Utils;

internal readonly struct Point3D
{
    public int X { get; private init; }
    public int Y { get; private init; }
    public int Z { get; private init; }

    public Point3D(int x, int y, int z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Point3D(string input)
    {
        var split = input.Split(',');
        X = int.Parse(split[0]);
        Y = int.Parse(split[1]);
        Z = int.Parse(split[2]);
    }

    public Point3D(int[] input)
    {
        X = input[0];
        Y = input[1];
        Z = input[2];
    }

    public Point3D Move(Point3D point)
    {
        return new(X + point.X, Y + point.Y, Z + point.Z);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Point3D other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        return false;
    }

    public override string ToString()
    {
        return $"X={X},Y={Y},Z={Z}";
    }

    public int GetManhattenDistanceToZero()
    {
        return int.Abs(X) + int.Abs(Y) + int.Abs(Z);
    }

    public int GetManhattenDistance(Point3D other)
    {
        return int.Abs(int.Max(X, other.X) - int.Min(X, other.X)) +
               int.Abs(int.Max(Y, other.Y) - int.Min(Y, other.Y)) +
               int.Abs(int.Max(Z, other.Z) - int.Min(Z, other.Z));
    }

    public double GetStraighLineDistance(Point3D other)
    {
        var distance = Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2) + Math.Pow(Z - other.Z, 2);
        return Math.Sqrt(distance);
    }

    public static bool operator ==(Point3D left, Point3D right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Point3D left, Point3D right)
    {
        return !left.Equals(right);
    }
}