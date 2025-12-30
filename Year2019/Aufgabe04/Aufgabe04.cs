using AdventOfCode.Utils;

namespace AdventOfCode.Year2019
{
    internal class Aufgabe04 : IAufgabe
    {
        private readonly string _input;

        public Aufgabe04()
        {
            _input = Utilities.ReadInput(2019, 4)[0];
        }

        public string Calc()
        {
            var numbers = _input.GetUnsignedNumbers();
            int result = 0;

            for (int i = numbers[0]; i <= numbers[1]; i++)
            {
                if (Check(i))
                {
                    result++;
                }
            }

            return result.ToString();
        }

        private static bool Check(int number)
        {
            bool hasDouble = false;
            var numberString = number.ToString();

            var lastCharacter = numberString[0];
            foreach (var character in numberString.Skip(1))
            {
                if (character < lastCharacter)
                {
                    return false;
                }
                else if (character == lastCharacter)
                {
                    hasDouble = true;
                }

                lastCharacter = character;
            }

            return hasDouble;
        }
    }
}
