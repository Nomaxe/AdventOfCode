using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe06 : IAufgabe
{
    private readonly DictionaryHashSet<string, string> _items = new();

    public Aufgabe06()
    {
        var input = Utilities.ReadInput(2019, 6);
        foreach (var line in input)
        {
            var split = line.Split(')');
            _items.Add(split[0], split[1]);
        }
    }

    public string Calc()
    {
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
