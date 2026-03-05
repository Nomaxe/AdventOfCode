using System.Numerics;

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
        internal long GetDecimalNumber()
        {
            long result = 0;

            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i])
                {
                    continue;
                }

                result += (long)Math.Pow(2, list.Count - i - 1);
            }

            return result;
        }
    }

    extension<T>(List<T> list) where T : INumber<T>
    {
        internal long Mul()
        {
            long result = 1;

            foreach (var number in list)
            {
                result *= long.CreateChecked(number);
            }

            return result;
        }
    }
}
