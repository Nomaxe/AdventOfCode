using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Utils;

internal static class CharExtensions
{
    internal static int ToNumber(this char character)
    {
        return character - '0';
    }

    internal static Direction ToDirection(this char character)
    {
        return character switch
        {
            'R' => Direction.Right,
            'D' => Direction.Down,
            'L' => Direction.Left,
            'U' => Direction.Up,
            _ => throw new NotImplementedException()
        };
    }
}
