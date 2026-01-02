namespace AdventOfCode.Utils;

internal static class LinkedListExtensions
{
    extension<T>(LinkedListNode<T> node)
    {
        internal LinkedListNode<T> GetNext(LinkedList<T> list)
        {
            return node.Next ?? list.First ?? throw new NullReferenceException();
        }

        internal LinkedListNode<T> GetPrevious(LinkedList<T> list)
        {
            return node.Previous ?? list.Last ?? throw new NullReferenceException();
        }
    }
}
