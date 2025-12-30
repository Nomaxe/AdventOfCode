namespace AdventOfCode.Utils;

internal static class LinkedListExtensions
{
    internal static LinkedListNode<T> GetNext<T>(this LinkedListNode<T> node, LinkedList<T> list)
    {
        return node.Next ?? list.First ?? throw new NullReferenceException();
    }

    internal static LinkedListNode<T> GetPrevious<T>(this LinkedListNode<T> node, LinkedList<T> list)
    {
        return node.Previous ?? list.Last ?? throw new NullReferenceException();
    }
}
