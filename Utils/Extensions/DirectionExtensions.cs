using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Utils;

internal static class DirectionExtensions
{
    internal static Direction TurnRight(this Direction direction)
    {
        var value = (int)direction + 1;

        if (value > 3)
        {
            value = 0;
        }

        return (Direction)value;
    }

    internal static Direction TurnLeft(this Direction direction)
    {
        var value = (int)direction - 1;

        if (value < 0)
        {
            value = 3;
        }

        return (Direction)value;
    }

    internal static Direction Reverse(this Direction direction)
    {
        var value = (int)direction + 2;

        if (value > 3)
        {
            value -= 4;
        }

        return (Direction)value;
    }
}
