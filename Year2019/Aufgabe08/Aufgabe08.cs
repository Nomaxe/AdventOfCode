using AdventOfCode.Utils;

namespace AdventOfCode.Year2019
{
    internal class Aufgabe08 : IAufgabe
    {
        private readonly string _input;

        public Aufgabe08()
        {
            _input = Utilities.ReadInput(2019, 8)[0];
        }

        public string Calc()
        {
            int x = 0;
            int y = 0;
            int zeroCount = int.MaxValue;
            int result = 0;
            int currentZero = 0;
            int currentOne = 0;
            int currentTwo = 0;

            foreach (var character in _input)
            {
                var number = character.ToNumber();

                switch (number)
                {
                    case 0:
                        currentZero++;
                        break;
                    case 1:
                        currentOne++;
                        break;
                    case 2:
                        currentTwo++;
                        break;
                    default:
                        throw new NotImplementedException();
                }

                x++;

                if (x >= 25)
                {
                    x = 0;
                    y++;

                    if (y >= 6)
                    {

                        if (currentZero < zeroCount)
                        {
                            zeroCount = currentZero;
                            result = currentOne * currentTwo;
                        }

                        y = 0;
                        currentZero = 0;
                        currentOne = 0;
                        currentTwo = 0;
                    }
                }
            }

            return result.ToString();
        }
    }
}
