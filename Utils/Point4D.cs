using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Utils;

internal readonly struct Point4D
{
    public int A { get; private init; }
    public int B { get; private init; }
    public int C { get; private init; }
    public int D { get; private init; }

    public Point4D(int a, int b, int c, int d)
    {
        A = a;
        B = b;
        C = c;
        D = d;
    }

    public Point4D(string input)
    {
        var split = input.Split(',');
        A = int.Parse(split[0]);
        B = int.Parse(split[1]);
        C = int.Parse(split[2]);
        D = int.Parse(split[3]);
    }

    public Point4D(int[] input)
    {
        A = input[0];
        B = input[1];
        C = input[2];
        D = input[3];
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(A, B, C, D);
    }

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is Point4D other)
        {
            return A == other.A && B == other.B && C == other.C && D == other.D;
        }

        return false;
    }

    public override string ToString()
    {
        return $"A={A},B={B},C={C},D={D}";
    }

    public int GetManhattenDistanceToZero()
    {
        return int.Abs(A) + int.Abs(B) + int.Abs(C) + int.Abs(D);
    }

    public int GetManhattenDistance(Point4D other)
    {
        return int.Abs(int.Max(A, other.A) - int.Min(A, other.A)) +
               int.Abs(int.Max(B, other.B) - int.Min(B, other.B)) +
               int.Abs(int.Max(C, other.C) - int.Min(C, other.C)) +
               int.Abs(int.Max(D, other.D) - int.Min(D, other.D));
    }

    public static bool operator ==(Point4D left, Point4D right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Point4D left, Point4D right)
    {
        return !left.Equals(right);
    }
}