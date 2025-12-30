using AdventOfCode.Utils;

namespace AdventOfCode.Year2017
{
    internal class Aufgabe02b : IAufgabe
    {
        private readonly string[] _input;

        public Aufgabe02b()
        {
            _input = Utilities.ReadInput(2017, 2);
        }

        public string Calc()
        {
            int result = 0;

            foreach (var line in _input)
            {
                result += Calc(line);
            }

            return result.ToString();
        }

        private static int Calc(string line)
        {
            var numbers = line.GetUnsignedNumbers();

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = i + 1; j < numbers.Length; j++)
                {
                    var max = int.Max(numbers[i], numbers[j]);
                    var min = int.Min(numbers[i], numbers[j]);

                    if (max % min == 0)
                    {
                        return max / min;
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
