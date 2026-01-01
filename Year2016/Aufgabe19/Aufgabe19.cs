using AdventOfCode.Utils;

namespace AdventOfCode.Year2016;

internal class Aufgabe19 : IAufgabe
{
    private readonly LinkedList<int> _list;

    public Aufgabe19()
    {
        var input = Utilities.ReadInputAsT<int>(2016, 19);
        _list = new(Enumerable.Range(1, input));
    }

    public string Calc()
    {
        //Optimierung = https://www.youtube.com/watch?v=uCsD3ZGzMgE

        var currentNode = _list.First ?? throw new NotImplementedException();

        while (_list.Count > 1)
        {
            _list.Remove(currentNode.GetNext(_list));
            currentNode = currentNode.GetNext(_list);
        }

        return currentNode.Value.ToString();
    }
}
