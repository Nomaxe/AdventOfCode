using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2018;

internal class Aufgabe07 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryList<char, char> _steps;

    public Aufgabe07()
    {
        _input = Utilities.ReadInput(2018, 7);
        _steps = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            _steps.Add(line[36], line[5]);
            _steps.AddKey(line[5]);
        }

        StringBuilder builder = new();

        do
        {
            var character = _steps.Where(x => x.Value.Count == 0).OrderBy(x => x.Key).Select(x => x.Key).First();
            builder.Append(character);
            _steps.RemoveItemAtAllKeys(character);
            _steps.RemoveAll(character);
        } while (_steps.Count > 0);

        return builder.ToString();
    }
}
