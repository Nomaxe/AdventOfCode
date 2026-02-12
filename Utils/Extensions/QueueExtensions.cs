namespace AdventOfCode.Utils;

public static class QueueExtensions
{
    extension<T>(Queue<T> queue)
    {
        public Queue<T> GetNewQueue(int count)
        {
            Queue<T> newQueue = new(count);

            foreach (var item in queue.Take(count))
            {
                newQueue.Enqueue(item);
            }

            return newQueue;
        }
    }
}
