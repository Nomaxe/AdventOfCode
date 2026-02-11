using AdventOfCode.Utils;

namespace AdventOfCode.Year2017;

internal class Aufgabe17 : IAufgabe
{
    private readonly string _input;

    public Aufgabe17()
    {
        _input = Utilities.ReadInputAsString(2017, 17);
    }

    public string Calc()
    {
        int times = int.Parse(_input);
        LinkedList<int> list = new();
        var currentNode = list.AddFirst(0);

        for (int i = 1; i <= 2017; i++)
        {
            for (int j = 0; j < times; j++)
            {
                currentNode = currentNode.GetNext(list);
            }

            currentNode = list.AddAfter(currentNode, i);
        }

        return currentNode.GetNext(list).Value.ToString();
    }
}
