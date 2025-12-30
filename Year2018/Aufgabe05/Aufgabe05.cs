using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2018;

internal class Aufgabe05 : IAufgabe
{
    public string _polymer;

    public Aufgabe05()
    {
        _polymer = Utilities.ReadInput(2018, 5)[0];
    }

    public string Calc()
    {
        bool didChange;

        do
        {
            StringBuilder builder = new(_polymer.Length);
            didChange = false;

            for (int i = 0; i < _polymer.Length; i++)
            {
                if (i == _polymer.Length - 1)
                {
                    builder.Append(_polymer[i]);
                }
                else if (!(_polymer[i] != _polymer[i + 1] && char.ToUpper(_polymer[i]) == char.ToUpper(_polymer[i + 1])))
                {
                    builder.Append(_polymer[i]);
                }
                else
                {
                    didChange = true;
                    i++;
                }
            }

            _polymer = builder.ToString();
        } while (didChange);

        return _polymer.Length.ToString();
    }
}
