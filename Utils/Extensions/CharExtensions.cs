using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Utils;

internal static partial class CharExtensions
{
    extension(char character)
    {
        internal int ToNumber()
        {
            return character - '0';
        }

        internal Direction ToDirection()
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
}
