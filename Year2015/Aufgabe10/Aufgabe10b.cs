using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2015;

internal class Aufgabe10b : IAufgabe
{
    private string _value;

    public Aufgabe10b()
    {
        _value = Utilities.ReadInput(2015, 10)[0];
    }

    public string Calc()
    {
        for (int i = 0; i < 50; i++)
        {
            StringBuilder builder = new(_value.Length);

            int count = 1;
            char currentNumber = _value[0];

            foreach (var character in _value.Skip(1))
            {
                if (character != currentNumber)
                {
                    builder.Append($"{count}{currentNumber}");

                    currentNumber = character;
                    count = 1;
                }
                else
                {
                    count++;
                }
            }

            builder.Append($"{count}{currentNumber}");

            _value = builder.ToString();
        }

        return _value.Length.ToString();
    }
}
