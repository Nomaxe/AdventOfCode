using AdventOfCode.Utils;

namespace AdventOfCode.Year2020;

internal class Aufgabe23b : IAufgabe
{
    private readonly List<int> _input;
    private readonly LinkedList<int> _numbers;
    private readonly LinkedListNode<int>[] _lookup;
    private LinkedListNode<int> _currentNode;

#pragma warning disable CS8618
    public Aufgabe23b()
    {
        _input = Utilities.ReadInputAsSingleNumbers<int>(2020, 23);
        _numbers = new();
        _lookup = new LinkedListNode<int>[1_000_001];
    }
#pragma warning restore CS8618

    public string Calc()
    {
        foreach (var number in _input)
        {
            var node = _numbers.AddLast(number);
            _lookup[number] = node;
        }
        for (int i = 10; i <= 1_000_000; i++)
        {
            var node = _numbers.AddLast(i);
            _lookup[i] = node;
        }
        _currentNode = _numbers.First!;

        for (int i = 0; i < 10_000_000; i++)
        {
            var pickedCups = PickUpCups();
            var destinationNumber = _currentNode.Value - 1;
            if (destinationNumber == 0)
            {
                destinationNumber = 1_000_000;
            }


            while (pickedCups.Contains(destinationNumber))
            {
                destinationNumber--;
                if (destinationNumber == 0)
                {
                    destinationNumber = 1_000_000;
                }
            }

            var destinationNode = _lookup[destinationNumber];
            destinationNode = _numbers.AddAfter(destinationNode, pickedCups[0]);
            _lookup[pickedCups[0]] = destinationNode;
            destinationNode = _numbers.AddAfter(destinationNode, pickedCups[1]);
            _lookup[pickedCups[1]] = destinationNode;
            destinationNode = _numbers.AddAfter(destinationNode, pickedCups[2]);
            _lookup[pickedCups[2]] = destinationNode;

            _currentNode = _currentNode.GetNext(_numbers);
        }

        var node1 = _lookup[1].GetNext(_numbers);
        var node2 = node1.GetNext(_numbers);

        return ((ulong)node1.Value * (ulong)node2.Value).ToString();
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
