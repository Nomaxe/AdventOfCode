using AdventOfCode.Utils;

namespace AdventOfCode.Year2017
{
    internal class Aufgabe02 : IAufgabe
    {
        private readonly string[] _input;

        public Aufgabe02()
        {
            _input = Utilities.ReadInput(2017, 2);
        }

        public string Calc()
        {
            int result = 0;

            foreach (var line in _input)
            {
                var numbers = line.GetUnsignedNumbers();

                int min = int.MaxValue;
                int max = 0;

                foreach (var number in numbers)
                {
                    min = int.Min(min, number);
                    max = int.Max(max, number);
                }

                result += max - min;
            }

            return result.ToString();
        }
    }
}
