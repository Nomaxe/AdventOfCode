namespace AdventOfCode.Utils;

internal static partial class ListExtensions
{
    extension<T>(List<T> list) where T : notnull
    {
        internal bool IsDistinct()
        {
            T value = list[0];

            for (int i = 1; i < list.Count; i++)
            {
                if (!value.Equals(list[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }

    extension(List<bool> list)
    {
        internal ulong GetDecimalNumber()
        {
            ulong result = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i])
                {
                    continue;
                }

                result += (ulong)Math.Pow(2, list.Count - i - 1);
            }

            return result;
        }
    }
}
