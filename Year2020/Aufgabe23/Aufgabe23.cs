using AdventOfCode.Utils;
using System.Text;

namespace AdventOfCode.Year2020;

internal class Aufgabe23 : IAufgabe
{
    private readonly LinkedList<int> _numbers;
    private LinkedListNode<int> _currentNode;

    public Aufgabe23()
    {
        var input = Utilities.ReadInputAsSingleNumbers<int>(2020, 23);
        _numbers = new(input);
        _currentNode = _numbers.First!;
    }

    public string Calc()
    {
        for (int i = 0; i < 100; i++)
        {
            var pickedCups = PickUpCups();
            var destinationNumber = _currentNode.Value - 1;
            if (destinationNumber == 0)
            {
                destinationNumber = 9;
            }


            while (pickedCups.Contains(destinationNumber))
            {
                destinationNumber--;
                if (destinationNumber == 0)
                {
                    destinationNumber = 9;
                }
            }

            var destinationNode = _numbers.Find(destinationNumber)!;
            destinationNode = _numbers.AddAfter(destinationNode, pickedCups[0]);
            destinationNode = _numbers.AddAfter(destinationNode, pickedCups[1]);
            _numbers.AddAfter(destinationNode, pickedCups[2]);

            _currentNode = _currentNode.GetNext(_numbers);
        }

        StringBuilder before = new();
        StringBuilder after = new();
        bool before1 = true;

        foreach (var number in _numbers)
        {
            if (number == 1)
            {
                before1 = false;
                continue;
            }

            if (before1)
            {
                before.Append(number);
            }
            else
            {
                after.Append(number);
            }
        }

        return after.ToString() + before.ToString();
    }

    private int[] PickUpCups()
    {
        int[] pickedCups = new int[3];
        var node1 = _currentNode.GetNext(_numbers);
        pickedCups[0] = node1.Value;
        var node2 = node1.GetNext(_numbers);
        pickedCups[1] = node2.Value;
        var node3 = node2.GetNext(_numbers);
        pickedCups[2] = node3.Value;

        _numbers.Remove(node1);
        _numbers.Remove(node2);
        _numbers.Remove(node3);

        return pickedCups;
    }
}
