using static AdventOfCode.Utils.Enums;

namespace AdventOfCode.Utils;

internal static class DirectionExtensions
{
    extension(Direction direction)
    {
        internal Direction TurnRight()
        {
            var value = (int)direction + 1;

            if (value > 3)
            {
                value = 0;
            }

            return (Direction)value;
        }

        internal Direction TurnLeft()
        {
            var value = (int)direction - 1;

            if (value < 0)
            {
                value = 3;
            }

            return (Direction)value;
        }

        internal Direction Reverse()
        {
            var value = (int)direction + 2;

            if (value > 3)
            {
                value -= 4;
            }

            return (Direction)value;
        }
    }
}
