using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe06 : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryHashSet<string, string> _items;

    public Aufgabe06()
    {
        _input = Utilities.ReadInput(2019, 6);
        _items = new(_input.Length);
    }

    public string Calc()
    {
        foreach (var line in _input)
        {
            var split = line.Split(')');
            _items.Add(split[0], split[1]);
        }

        List<string> checkItems = ["COM"];
        int length = 0;
        int result = 0;

        while (checkItems.Count > 0)
        {
            List<string> nextCheckItems = [];

            result += checkItems.Count * length;
            foreach (var item in checkItems)
            {
                nextCheckItems.AddRange(_items.GetItems(item));
            }

            checkItems = nextCheckItems;
            length++;
        }

        return result.ToString();
    }
}
