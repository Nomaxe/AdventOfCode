namespace AdventOfCode.Utils.Extensions;

internal static partial class ArrayExtensions
{
    extension(bool[] list)
    {
        internal ulong GetDecimalNumber()
        {
            ulong result = 0;

            for (int i = 0; i < list.Length; i++)
            {
                if (!list[i])
                {
                    continue;
                }

                result += (ulong)Math.Pow(2, list.Length - i - 1);
            }

            return result;
        }
    }
}
