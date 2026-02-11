using AdventOfCode.Utils;

namespace AdventOfCode.Year2019;

internal class Aufgabe06b : IAufgabe
{
    private readonly string[] _input;
    private readonly DictionaryHashSet<string, string> _items = new();
    private string _start = string.Empty;

    public Aufgabe06b()
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

            if (split[1] == "YOU")
            {
                _start = split[0];
            }
        }

        HashSet<string> alreadyChecked = ["YOU"];
        List<string> checkItems = [_start];
        int length = 0;

        while (checkItems.Count > 0)
        {
            List<string> nextCheckItems = [];

            foreach (var item in checkItems)
            {
                if (_items.Contains(item, "SAN"))
                {
                    return length.ToString();
                }

                foreach (var nextItem in _items.GetItems(item))
                {
                    if (!alreadyChecked.Contains(nextItem))
                    {
                        nextCheckItems.Add(nextItem);
                        alreadyChecked.Add(nextItem);
                    }
                }
                foreach (var nextItem in _items.GetKeysOfItem(item))
                {
                    if (!alreadyChecked.Contains(nextItem))
                    {
                        nextCheckItems.Add(nextItem);
                        alreadyChecked.Add(nextItem);
                    }
                }
            }

            checkItems = nextCheckItems;
            length++;
        }

        throw new NotImplementedException();
    }
}
